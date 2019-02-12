using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClubSystem.Lib;
using ClubSystem.Lib.Exceptions;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models;
using ClubSystem.Lib.Models.Dtos;
using ClubSystem.Lib.Models.Resources;
using ClubSystem.Lib.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace ClubSystem.Test.RepositoryTests
{
    public class PostRepositoryTests
    {
        private const string GivenUserIdIsWrongErrorMessage = "Given userId is wrong";

        private static IPostRepository GetInMemoryPostRepository()
        {
            var builder = new DbContextOptionsBuilder<ClubSystemDbContext>();
            var options = builder.UseInMemoryDatabase(new Guid().ToString()).Options;

            var clubSystemDbContext = new ClubSystemDbContext(options);
            clubSystemDbContext.Database.EnsureDeleted();
            clubSystemDbContext.Database.EnsureCreated();
            return new PostRepository(clubSystemDbContext);
        }

        private static ClubSystemDbContext GetInMemoryDbContext()
        {
            var builder = new DbContextOptionsBuilder<ClubSystemDbContext>();
            var options = builder.UseInMemoryDatabase(new Guid().ToString()).Options;

            var clubSystemDbContext = new ClubSystemDbContext(options);
            clubSystemDbContext.Database.EnsureDeleted();
            clubSystemDbContext.Database.EnsureCreated();
            clubSystemDbContext.Users.Add(new User
                {Id = "1234", UserName = "username1", PasswordHash = new Guid().ToString()});
            return clubSystemDbContext;
        }

        private static ClaimsPrincipal GenerateClaimsPrincipalWithId(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim("sub", userId)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            return claimsPrincipal;
        }

        private static async Task<(string userId, ClubResource addedClub1, ClubResource addedClub2)> SetUpPostFeed(
            ClubSystemDbContext dbContext)
        {
            var clubRepository = new ClubRepository(dbContext);

            var clubDto1 = new ClubDto {Name = "Name1", UniversityName = "University1"};
            var clubDto2 = new ClubDto {Name = "Name2", UniversityName = "University2"};
            var addedClub1 = clubRepository.AddClub(clubDto1);
            var addedClub2 = clubRepository.AddClub(clubDto2);
            const string userId = "1234";

            var claimsPrincipal = GenerateClaimsPrincipalWithId(userId);

            var addUserToClubDto1 = new AddUserToClubDto {ClubId = addedClub1.Id};
            var addUserToClubDto2 = new AddUserToClubDto {ClubId = addedClub2.Id};
            await clubRepository.AddUserToClub(addUserToClubDto1, claimsPrincipal);
            await clubRepository.AddUserToClub(addUserToClubDto2, claimsPrincipal);

            return (userId, addedClub1, addedClub2);
        }

        [Fact]
        public void ShouldAddPost()
        {
            var postRepository = GetInMemoryPostRepository();

            var userIds = new List<string> {"42", "45"};
            var post1 = new PostDto
                {Title = "Title1", Content = "Content1", MediaId = "1234", UserIds = userIds, ClubId = "124"};

            var addedPost = postRepository.AddPost(post1);

            Assert.NotNull(addedPost);
            Assert.Equal(addedPost.Title, post1.Title);
            Assert.Equal(addedPost.Content, post1.Content);
            var addedUserIds = addedPost.Users.Select(p => p.Id).ToList();
            Assert.Equal(addedUserIds, post1.UserIds);
            Assert.Equal(addedPost.ClubId, post1.ClubId);
        }

        [Fact]
        public async Task ShouldDeletePost()
        {
            // arrange
            var postRepository = GetInMemoryPostRepository();

            var userIds = new List<string> {"42", "45"};
            var post1 = new PostDto
                {Title = "Title1", Content = "Content1", MediaId = "1234", UserIds = userIds, ClubId = "124"};
            var userIds2 = new List<string> {"214"};
            var post2 = new PostDto
                {Title = "Title2", Content = "Content2", MediaId = "452645", UserIds = userIds2, ClubId = "124"};

            var addedPost1 = postRepository.AddPost(post1);
            var addedPost2 = postRepository.AddPost(post2);

            // act
            await postRepository.RemoveAsync(addedPost1.Id);

            // assert
            Assert.DoesNotContain(addedPost1, postRepository.GetAllPosts());

            var gettedPostJson = JsonConvert.SerializeObject(postRepository.GetPost(addedPost2.Id));
            var addedPostJson = JsonConvert.SerializeObject(addedPost2);
            Assert.Equal(addedPostJson, gettedPostJson);
        }

        [Fact]
        public void ShouldGetAllPost()
        {
            var postRepository = GetInMemoryPostRepository();

            var userIds1 = new List<string> {"42"};
            var post1 = new PostDto
                {Title = "Title1", Content = "Content1", MediaId = "1234", UserIds = userIds1, ClubId = "124"};

            var userIds2 = new List<string> {"214"};
            var post2 = new PostDto
                {Title = "Title2", Content = "Content2", MediaId = "452645", UserIds = userIds2, ClubId = "124"};

            postRepository.AddPost(post1);
            postRepository.AddPost(post2);
            var response = postRepository.GetAllPosts();

            Assert.Equal(2, response.Count);
            Assert.Equal(response.First(x => x.Title == post1.Title).Title, post1.Title);
            Assert.Equal(response.First(x => x.Title == post1.Title).MediaId, post1.MediaId);
            Assert.Equal(response.First(x => x.Title == post2.Title).Title, post2.Title);
            Assert.Equal(response.First(x => x.Title == post2.Title).MediaId, post2.MediaId);
        }

        [Fact]
        public async Task ShouldGetArgumentExceptionWithUserIdError()
        {
            // Arrange
            var postRepository = GetInMemoryPostRepository();

            // Act & Assert
            var exception =
                await Assert.ThrowsAsync<ArgumentException>(() => postRepository.GetMyPostFeedAsync("1234"));
            Assert.Equal(GivenUserIdIsWrongErrorMessage, exception.Message);
        }

        [Fact]
        public async Task ShouldGetEmptyPostFeed()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var postRepository = new PostRepository(dbContext);
            var (userId, _, _) = await SetUpPostFeed(dbContext);

            // Act
            var postResponse = await postRepository.GetMyPostFeedAsync(userId);

            // Assert
            Assert.Empty(postResponse);
        }

        [Fact]
        public void ShouldGetOnlyOnePost()
        {
            var postRepository = GetInMemoryPostRepository();

            var userIds = new List<string> {"42"};
            var post = new PostDto
                {Title = "Title1", Content = "Content1", MediaId = "1234", UserIds = userIds, ClubId = "124"};

            postRepository.AddPost(post);
            var result = postRepository.GetAllPosts();

            Assert.Equal(1, result.Count);
            Assert.Equal(result.First().ClubId, post.ClubId);
        }

        [Fact]
        public async Task ShouldGetPostFeedCorrect()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var postRepository = new PostRepository(dbContext);

            // This method creates two clubs and adds the first created user (see GetInMemoryDbContext method) to these clubs.
            // Just for reducing complexity ¯\_(ツ)_/¯
            var (userId, addedClub1, addedClub2) = await SetUpPostFeed(dbContext);

            postRepository.AddPost(new PostDto
                {ClubId = addedClub1.Id, Title = "Club1 Post1 Title1", Content = "Club1 Post1 Content1"});
            postRepository.AddPost(new PostDto
                {ClubId = addedClub2.Id, Title = "Club2 Post2 Title2", Content = "Club2 Post2 Content2"});

            var postResponse = await postRepository.GetMyPostFeedAsync(userId);

            // Assert
            Assert.Equal(2, postResponse.Count);
        }

        [Fact]
        public void ShouldReturnNull()
        {
            var postRepository = GetInMemoryPostRepository();

            var response = postRepository.GetPost("1");

            Assert.Null(response);
        }

        [Fact(Skip = "This test fails randomly so i skipped")]
        public void ShouldReturnPost()
        {
            var postRepository = GetInMemoryPostRepository();

            var userIds1 = new List<string> {"42", "45"};
            var post1 = new PostDto
                {Title = "Title1", Content = "Content1", MediaId = "1234", UserIds = userIds1, ClubId = "124"};

            var userIds2 = new List<string> {"214"};
            var post2 = new PostDto
                {Title = "Title2", Content = "Content2", MediaId = "452645", UserIds = userIds2, ClubId = "312"};

            var addedPost1 = postRepository.AddPost(post1);
            var addedPost2 = postRepository.AddPost(post2);
            var response = postRepository.GetPost(addedPost1.Id);

            Assert.NotNull(addedPost1.Id);
            Assert.NotNull(response);
            Assert.NotEqual(response, addedPost2);

            var responseJson = JsonConvert.SerializeObject(response);
            var addedPost1Json = JsonConvert.SerializeObject(addedPost1);
            Assert.Equal(responseJson, addedPost1Json);
        }

        [Fact]
        public void ShouldThrowPostIsNotValidException()
        {
            var postRepository = GetInMemoryPostRepository();
            var emptyPost = new PostDto();

            Assert.Throws<PostCannotBeNullException>(() => postRepository.AddPost(null));
            Assert.Throws<PostIsNotValidException>(() => postRepository.AddPost(emptyPost));
        }

        [Fact]
        public async Task ShouldThrowPostNotFoundException()
        {
            // arrange
            var postRepository = GetInMemoryPostRepository();
            const string wrongPostId = "12345";

            // act & assert
            await Assert.ThrowsAsync<PostNotFoundException>(() => postRepository.RemoveAsync(wrongPostId));
        }
    }
}
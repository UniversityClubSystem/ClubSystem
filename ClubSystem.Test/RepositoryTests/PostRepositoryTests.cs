using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubSystem.Lib;
using ClubSystem.Lib.Exceptions;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models.Dtos;
using ClubSystem.Lib.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace ClubSystem.Test.RepositoryTests
{
    public class PostRepositoryTests
    {
        [Fact]
        public void ShouldAddPost()
        {
            var postRepository = GetInMemoryPostRepository();

            var userIds = new List<string> { "42", "45" };
            var post1 = new PostDto { Title = "Title1", Content = "Content1", MediaId = "1234", UserIds = userIds, ClubId = "124" };

            var addedPost = postRepository.AddPost(post1);

            Assert.NotNull(addedPost);
            Assert.Equal(addedPost.Title, post1.Title);
            Assert.Equal(addedPost.Content, post1.Content);
            var addedUserIds = addedPost.Users.Select(p => p.Id).ToList();
            Assert.Equal(addedUserIds, post1.UserIds);
            Assert.Equal(addedPost.ClubId, post1.ClubId);
        }

        [Fact]
        public void ShouldGetOnlyOnePost()
        {
            var postRepository = GetInMemoryPostRepository();
            
            var userIds = new List<string> { "42" };
            var post = new PostDto { Title = "Title1", Content = "Content1", MediaId = "1234", UserIds = userIds, ClubId = "124" };

            postRepository.AddPost(post);
            var result = postRepository.GetAllPosts();
            
            Assert.Equal(1, result.Count);
            Assert.Equal(result.First().ClubId, post.ClubId);
        }

        [Fact]
        public void ShouldGetAllPost()
        {
            var postRepository = GetInMemoryPostRepository();

            var userIds1 = new List<string> { "42" };
            var post1 = new PostDto { Title = "Title1", Content = "Content1", MediaId = "1234", UserIds = userIds1, ClubId = "124" };

            var userIds2 = new List<string> { "214" };
            var post2 = new PostDto { Title = "Title2", Content = "Content2", MediaId = "452645", UserIds = userIds2, ClubId = "124" };

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
        public void ShouldReturnNull()
        {
            var postRepository = GetInMemoryPostRepository();

            var response = postRepository.GetPost("1");

            Assert.Null(response);
        }

        [Fact]
        public void ShouldThrowPostIsNotValidException()
        {
            var postRepository = GetInMemoryPostRepository();
            PostDto emptyPost = new PostDto();

            Assert.Throws<PostCannotBeNullException>(() => postRepository.AddPost(null));
            Assert.Throws<PostIsNotValidException>(() => postRepository.AddPost(emptyPost));
        }

        [Fact]
        public void ShouldReturnPost()
        {
            IPostRepository postRepository = GetInMemoryPostRepository();

            var userIds1 = new List<string> { "42", "45" };
            var post1 = new PostDto { Title = "Title1", Content = "Content1", MediaId = "1234", UserIds = userIds1, ClubId = "124" };

            var userIds2 = new List<string> { "214" };
            var post2 = new PostDto { Title = "Title2", Content = "Content2", MediaId = "452645", UserIds = userIds2, ClubId = "312" };

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
        public async Task ShouldGetEmptyPostFeed()
        {
            // Arrange
            var postRepository = GetInMemoryPostRepository();
            
            // Act
            var postResponse = await postRepository.GetMyPostFeedAsync("1234");
            
            // Assert
            Assert.Empty(postResponse);
        }
        
        [Fact]
        public async Task ShouldGetPostFeedWithOnePost()
        {
            // Arrange
            var postRepository = GetInMemoryPostRepository();
            
            var userIds1 = new List<string> { "42", "45" };
            var userIds2 = new List<string> { "43", "45" };
            var post1 = new PostDto { Title = "Title1", Content = "Content1", MediaId = "1234", UserIds = userIds1, ClubId = "4" };
            var post2 = new PostDto { Title = "Title2", Content = "Content2", MediaId = "542", UserIds = userIds1, ClubId = "3" };
            var post3 = new PostDto { Title = "Title3", Content = "Content3", MediaId = "56", UserIds = userIds2, ClubId = "5" };
            var post4 = new PostDto { Title = "Title4", Content = "Content4", MediaId = "6758", UserIds = userIds1, ClubId = "3" };

            var addedPost1 = postRepository.AddPost(post1);
            var addedPost2 = postRepository.AddPost(post2);
            var addedPost3 = postRepository.AddPost(post3);
            var addedPost4 = postRepository.AddPost(post4);
            
            if (addedPost1.Users.ElementAt(0) != null)
            {
                // Act
                var postResponse1 = await postRepository.GetMyPostFeedAsync(userIds2.ElementAt(0)); // user with userId 43
                var postResponse2 = await postRepository.GetMyPostFeedAsync(userIds1.ElementAt(0)); // user with userId 42
              
                // Assert
                Assert.Single(postResponse1);
                Assert.Equal(3, postResponse2.Count);
            }
        }
        
        [Fact]
        public async Task ShouldGetPostFeedCorrect()
        {
            // Arrange
            var postRepository = GetInMemoryPostRepository();

            // Act
            var postResponse = await postRepository.GetMyPostFeedAsync("1234");

            // Assert
            Assert.Equal(2, postResponse.Count);
        }

        private IPostRepository GetInMemoryPostRepository()
        {
            var builder = new DbContextOptionsBuilder<ClubSystemDbContext>();
            var options = builder.UseInMemoryDatabase(new Guid().ToString()).Options;

            ClubSystemDbContext clubSystemDbContext = new ClubSystemDbContext(options);
            clubSystemDbContext.Database.EnsureDeleted();
            clubSystemDbContext.Database.EnsureCreated();
            return new PostRepository(clubSystemDbContext);
        }
    }
}
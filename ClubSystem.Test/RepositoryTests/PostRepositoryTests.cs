using System;
using System.Collections.Generic;
using System.Linq;
using ClubSystem.Lib;
using ClubSystem.Lib.Exceptions;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models.Dtos;
using ClubSystem.Lib.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClubSystem.Test.RepositoryTests
{
    public class PostRepositoryTests
    {
        [Fact]
        public void ShouldAddPost()
        {
            var postRepository = GetInMemoryPostRepository();

            var userIds = new List<string> { "42" };
            var clubId = "124";
            var post1 = new PostDto { Title = "Title1", Text = "Text1", MediaId = "1234", UserIds = userIds, ClubId = clubId };

            var addedPost = postRepository.AddPost(post1);

            Assert.NotNull(addedPost);
            Assert.Equal(addedPost.Title, post1.Title);
            Assert.Equal(addedPost.Text, post1.Text);
            var addedUserIds = addedPost.Users.Select(p => p.Id).ToList();
            Assert.Equal(addedUserIds, post1.UserIds);
            Assert.Equal(addedPost.ClubId, post1.ClubId);
        }

        [Fact]
        public void ShouldGetOnlyOnePost()
        {
            var postRepository = GetInMemoryPostRepository();
            
            var userIds = new List<string> { "42" };
            var clubId = "124";
            var post = new PostDto { Title = "Title1", Text = "Text1", MediaId = "1234", UserIds = userIds, ClubId = clubId };

            postRepository.AddPost(post);
            var result = postRepository.GetAllPosts();
            
            Assert.Equal(1, result.Count);
            // Assert.Equal(result.First().ClubId, post.ClubId); TODO: GetAllPosts sýnýfýna ClubId eklendiðinde aktifleþtirilecek.
        }

        [Fact]
        public void ShouldGetAllPost()
        {
            var postRepository = GetInMemoryPostRepository();

            var userIds1 = new List<string> { "42" };
            var clubId1 = "124";
            var post1 = new PostDto { Title = "Title1", Text = "Text1", MediaId = "1234", UserIds = userIds1, ClubId = clubId1 };

            var userIds2 = new List<string> { "214" };
            var clubId2 = "124";
            var post2 = new PostDto { Title = "Title2", Text = "Text2", MediaId = "452645", UserIds = userIds2, ClubId = clubId2 };

            postRepository.AddPost(post1);
            postRepository.AddPost(post2);
            var result = postRepository.GetAllPosts();
            
            Assert.Equal(2, result.Count);
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

            var userIds1 = new List<string> { "42" };
            var clubId1 = "124";
            var post1 = new PostDto { Title = "Title1", Text = "Text1", MediaId = "1234", UserIds = userIds1, ClubId = clubId1 };

            var userIds2 = new List<string> { "214" };
            var clubId2 = "312";
            var post2 = new PostDto { Title = "Title2", Text = "Text2", MediaId = "452645", UserIds = userIds2, ClubId = clubId2 };

            var addedPost1 = postRepository.AddPost(post1);
            var addedPost2 = postRepository.AddPost(post2);
            var response = postRepository.GetPost(addedPost1.Id);

            Assert.NotNull(addedPost1.Id);
            Assert.NotNull(response);
            //Assert.NotEqual(response, addedPost2);
            //Assert.Equal(response, addedPost1);
        }

        private IPostRepository GetInMemoryPostRepository()
        {
            DbContextOptions<ClubSystemDbContext> options;
            var builder = new DbContextOptionsBuilder<ClubSystemDbContext>();
            options = builder.UseInMemoryDatabase(new Guid().ToString()).Options;

            ClubSystemDbContext clubSystemDbContext = new ClubSystemDbContext(options);
            clubSystemDbContext.Database.EnsureDeleted();
            clubSystemDbContext.Database.EnsureCreated();
            return new PostRepository(clubSystemDbContext);
        }
    }
}
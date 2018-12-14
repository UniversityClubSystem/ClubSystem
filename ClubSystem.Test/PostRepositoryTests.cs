using ClubSystem.Lib;
using ClubSystem.Lib.Exceptions;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models.Entities;
using ClubSystem.Lib.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ClubSystem.Test
{
    public class PostRepositoryTests
    {
        [Fact]
        public void ShouldAddPost()
        {
            var mock = new Mock<IPostRepository>();
            var userPosts = new List<UserPost> { new UserPost { UserId = 15 } };
            var post = new Post { Title = "title1", Text = "Text1", MediaId = 12345, UserPosts = userPosts };

            mock.Setup(p => p.AddPost(post)).Returns(post);

            var response = mock.Object.AddPost(post);

            Assert.Equal(response, post);
        }

        [Fact]
        public void ShouldGetOnlyOnePost()
        {
            var mock = new Mock<IPostRepository>();
            var userPosts = new List<UserPost> { new UserPost { UserId = 3 }, new UserPost { UserId = 6 } };
            var post = new Post { Title = "Title1", Text = "Lorem ipsum", MediaId = 2423, UserPosts = userPosts };

            mock.Setup(p => p.AddPost(post)).Returns(post);
            var posts = new List<Post> { post };
            mock.Setup(p => p.GetAllPosts()).Returns(posts);

            var postRepository = mock.Object;
            postRepository.AddPost(post);
            var response = postRepository.GetAllPosts();

            Assert.Equal(response, posts);
        }

        [Fact]
        public void ShouldReturnNull()
        {
            var postRepository = GetInMemoryPostRepository();

            var response = postRepository.GetPost(1);

            Assert.Null(response);
        }

        [Fact]
        public void ShouldThrowPostCannotBeNullException()
        {
            var postRepository = GetInMemoryPostRepository();
            Post post1 = null;
            Post post2 = new Post();

            Assert.Throws<PostCannotBeNullException>(() => postRepository.AddPost(post1));
            Assert.Throws<PostCannotBeNullException>(() => postRepository.AddPost(post2));
        }

        [Fact]
        public void ShouldReturnPost()
        {
            IPostRepository postRepository = GetInMemoryPostRepository();

            var userPosts = new List<UserPost> { new UserPost { UserId = 12 } };
            var post1 = new Post { Id = 3, Title = "PostTitle1", Text = "PostText1", MediaId = 2432, UserPosts = userPosts };
            var post2 = new Post { Id = 4, Title = "PostTitle2", Text = "PostText2", MediaId = 343534 };

            postRepository.AddPost(post1);
            postRepository.AddPost(post2);

            var response = postRepository.GetPost(post1.Id);

            Assert.NotEqual(response, post2);
            Assert.Equal(response, post1);
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
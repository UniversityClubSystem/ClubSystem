using System;
using System.Collections.Generic;
using ClubSystem.Lib;
using ClubSystem.Lib.Exceptions;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models.Entities;
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

            var userPosts = new List<UserPost> { new UserPost { UserId = "42" } };
            var post1 = new Post { Title = "Title1", Text = "Text1", MediaId = 1234, UserPosts = userPosts };

            var addedPost = postRepository.AddPost(post1);

            Assert.NotNull(addedPost);
            Assert.Equal(addedPost.Title, post1.Title);
            Assert.Equal(addedPost.Text, post1.Text);
            Assert.Equal(addedPost.UserPosts, post1.UserPosts);
        }

        [Fact]
        public void ShouldGetOnlyOnePost()
        {
            var postRepository = GetInMemoryPostRepository();

            var userPosts = new List<UserPost> { new UserPost { UserId = "125" } };
            var post = new Post { Title = "Title1", Text = "Text1", MediaId = 1234, UserPosts = userPosts };

            postRepository.AddPost(post);
            var result = postRepository.GetAllPosts();
            
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void ShouldGetAllPost()
        {
            var postRepository = GetInMemoryPostRepository();

            var userPosts1 = new List<UserPost> { new UserPost { UserId = "125" } };
            var post1 = new Post { Title = "Title1", Text = "Text1", MediaId = 1234, UserPosts = userPosts1 };

            var userPosts2 = new List<UserPost> { new UserPost { UserId = "424" } };
            var post2 = new Post { Title = "Title1", Text = "Text1", MediaId = 1234, UserPosts = userPosts2 };

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
            Post emptyPost = new Post();

            Assert.Throws<PostCannotBeNullException>(() => postRepository.AddPost(null));
            Assert.Throws<PostIsNotValidException>(() => postRepository.AddPost(emptyPost));
        }

        [Fact]
        public void ShouldReturnPost()
        {
            IPostRepository postRepository = GetInMemoryPostRepository();

            var userPosts1 = new List<UserPost> { new UserPost { UserId = "12" } };
            var userPosts2 = new List<UserPost> { new UserPost { UserId = "12" } };
            var post1 = new Post { Title = "PostTitle1", Text = "PostText1", MediaId = 2432, UserPosts = userPosts1 };
            var post2 = new Post { Title = "PostTitle2", Text = "PostText2", MediaId = 343534, UserPosts = userPosts2 };

            var addedPost1 = postRepository.AddPost(post1);
            var addedPost2 = postRepository.AddPost(post2);
            var response = postRepository.GetPost(addedPost1.Id);

            Assert.NotNull(addedPost1.Id);
            Assert.NotNull(response);
            Assert.NotEqual(response, addedPost2);
            Assert.Equal(response, addedPost1);
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
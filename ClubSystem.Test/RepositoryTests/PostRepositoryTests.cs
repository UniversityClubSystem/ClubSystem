using System;
using System.Collections.Generic;
using System.Linq;
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
            var post1 = new PostDto { Title = "Title1", Text = "Text1", MediaId = "1234", UserIds = userIds, ClubId = "124" };

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
            var post = new PostDto { Title = "Title1", Text = "Text1", MediaId = "1234", UserIds = userIds, ClubId = "124" };

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
            var post1 = new PostDto { Title = "Title1", Text = "Text1", MediaId = "1234", UserIds = userIds1, ClubId = "124" };

            var userIds2 = new List<string> { "214" };
            var post2 = new PostDto { Title = "Title2", Text = "Text2", MediaId = "452645", UserIds = userIds2, ClubId = "124" };

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
            var post1 = new PostDto { Title = "Title1", Text = "Text1", MediaId = "1234", UserIds = userIds1, ClubId = "124" };

            var userIds2 = new List<string> { "214" };
            var post2 = new PostDto { Title = "Title2", Text = "Text2", MediaId = "452645", UserIds = userIds2, ClubId = "312" };

            var addedPost1 = postRepository.AddPost(post1);
            var addedPost2 = postRepository.AddPost(post2);
            var response = postRepository.GetPost(addedPost1.Id);

            Assert.NotNull(addedPost1.Id);
            Assert.NotNull(response);
            Assert.NotEqual(response, addedPost2);

            var responseJSON = JsonConvert.SerializeObject(response);
            var addedPost1JSON = JsonConvert.SerializeObject(addedPost1);
            Assert.Equal(responseJSON, addedPost1JSON);
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
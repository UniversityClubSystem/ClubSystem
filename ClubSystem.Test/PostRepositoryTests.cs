using System.Collections.Generic;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models.Entities;
using Moq;
using Xunit;

namespace ClubSystem.Test
{
    public class PostRepositoryTests
    {
        [Fact]
        public void ShouldAddPost()
        {
            var mock = new Mock<IPostRepository>();
            var userPosts = new List<UserPost> {new UserPost {UserId = 15}};
            var post = new Post {Title = "title1", Text = "Text1", MediaId = 12345, UserPosts = userPosts};

            mock.Setup(p => p.AddPost(post)).Returns(post);

            var response = mock.Object.AddPost(post);

            Assert.Equal(response, post);
        }

        [Fact]
        public void ShouldGetOnlyOnePost()
        {
            var mock = new Mock<IPostRepository>();
            var userPosts = new List<UserPost> {new UserPost {UserId = 3}, new UserPost {UserId = 6}};
            var post = new Post {Title = "Title1", Text = "Lorem ipsum", MediaId = 2423, UserPosts = userPosts};
            
            mock.Setup(p => p.AddPost(post)).Returns(post);
            var posts = new List<Post> {post};
            mock.Setup(p => p.GetAllPosts()).Returns(posts);

            var postRepository = mock.Object;
            postRepository.AddPost(post);
            var response = postRepository.GetAllPosts();

            Assert.Equal(response, posts);
        }
    }
}
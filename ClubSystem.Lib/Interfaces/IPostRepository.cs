using System.Collections.Generic;
using ClubSystem.Lib.Models.Entities;

namespace ClubSystem.Lib.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        ICollection<Post> GetAllPosts();
        Post GetPost(string id);
        Post AddPost(Post club);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using ClubSystem.Lib.Models.Dtos;
using ClubSystem.Lib.Models.Entities;
using ClubSystem.Lib.Models.Resources;

namespace ClubSystem.Lib.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        ICollection<PostResource> GetAllPosts();
        PostResource GetPost(string id);
        PostResource AddPost(PostDto postDto);
        Task<ICollection<PostResource>> GetMyPostFeedAsync(string userId);
        Task<ICollection<PostResource>> GetPostByClubIds(IEnumerable<string> clubIds);
        Task RemoveAsync(string postId);
    }
}
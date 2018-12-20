using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models;
using ClubSystem.Lib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace ClubSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly UserManager<User> _userManager;

        public PostController(UserManager<User> userManager, IPostRepository postRepository)
        {
            _postRepository = postRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var allPosts = _postRepository.GetAllPosts();

            return Ok(allPosts);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] Post post)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            post.UserPosts = new Collection<UserPost> {new UserPost {UserId = user?.Id}};

            var addedPost = _postRepository.AddPost(post);

            return Ok(addedPost);
        }

        [HttpGet("{id}")]
        public IActionResult GetPost(string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var post = _postRepository.GetPost(id);

            return Ok(post);
        }
    }
}
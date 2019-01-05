using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models;
using ClubSystem.Lib.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

            var allPostResources = _postRepository.GetAllPosts();

            return Ok(allPostResources);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AddPost([FromBody] PostDto postDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // TODO: move this logic to service !!!important!!!
            /**
             * JwtRegisteredClaimNames.Sub => JwtTokenGenerator
             * line 35 new Claim(JwtRegisteredClaimNames.Sub, user.Id)
             */
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            postDto.UserIds = new Collection<string> {user?.Id};

            var addedPost = _postRepository.AddPost(postDto);

            return Ok(addedPost);
        }

        [HttpGet("postFeed/{userId}"), Authorize]
        public async Task<IActionResult> GetMyPostFeed(string userId)
        {
            var postResource = await _postRepository.GetMyPostFeedAsync(userId);
            return Ok(postResource);
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
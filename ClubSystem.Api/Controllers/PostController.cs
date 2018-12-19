using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ClubSystem.Api.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var allPosts = _postRepository.GetAllPosts();
            
            return Ok(allPosts);
        }

        [HttpPost]
        public IActionResult AddPost([FromBody] Post post)
        {
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
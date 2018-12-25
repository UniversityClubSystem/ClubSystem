using System;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models;
using ClubSystem.Lib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using ClubSystem.Lib.Models.Resources;
using ClubSystem.Lib.Models.Dtos;

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
            
            var allPostResources = new Collection<PostResource>();
            foreach (var post in allPosts)
            {
                var postResource = new PostResource
                {
                    Id = post.Id,
                    ClubId = post.Club.Id,
                    CreatedBy = post.UserPosts.ElementAt(0).User,
                    CreatedDate = post.CreatedDate,
                    LastEditedBy = null,
                    LastModifiedDate = DateTime.Now,
                    Text = post.Text,
                    Title = post.Title
                };
                allPostResources.Add(postResource);
            }

            return Ok(allPostResources);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] PostDto postDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            /**
             * JwtRegisteredClaimNames.Sub => JwtTokenGenerator
             * line 35 new Claim(JwtRegisteredClaimNames.Sub, user.Id)
             */
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            // postDto.UserPosts = new Collection<UserPost> {new UserPost {UserId = user?.Id}};
            postDto.UserIds = new Collection<string> { user?.Id };

            var addedPost = _postRepository.AddPost(postDto);

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
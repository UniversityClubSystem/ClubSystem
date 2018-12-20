using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models;
using ClubSystem.Lib.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClubSystem.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly UserManager<User> _userManager;

        public ClubController(UserManager<User> userManager, IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetAllClubs()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var users = _clubRepository.GetAllClubs();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetClub(string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _clubRepository.GetClub(id);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddClub([FromBody] Club club)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            club.UserClubs = new Collection<UserClub> {new UserClub {UserId = user?.Id}};

            var newClub = _clubRepository.AddClub(club);

            return Ok(newClub);
        }
    }
}
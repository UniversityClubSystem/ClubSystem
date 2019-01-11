using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly UserManager<User> _userManager;

        public ClubController(UserManager<User> userManager, IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
            _userManager = userManager;
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AddClub([FromBody] ClubDto clubDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            clubDto.CreatedBy = user?.Id;

            var newClub = _clubRepository.AddClub(clubDto);

            return Ok(newClub);
        }

        [HttpGet]
        public IActionResult GetAllClubs()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var clubs = _clubRepository.GetAllClubs();

            return Ok(clubs);
        }

        [HttpGet("{id}")]
        public IActionResult GetClub(string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _clubRepository.GetClub(id);

            return Ok(user);
        }

        [HttpGet("byUser/{id}"), Authorize]
        public IActionResult GetClubsByUser(string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var clubs = _clubRepository.GetClubsByUser(id);

            return Ok(clubs);
        }

        [HttpGet("byUser/current"), Authorize]
        public IActionResult GetClubsByCurrentUser()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var clubs = _clubRepository.GetClubsByCurrentUser(User);

            return Ok(clubs);
        }

        [HttpPost("join"), Authorize]
        public async Task<IActionResult> AddUserToClub([FromBody] AddUserToClubDto addUserToClubDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _clubRepository.AddUserToClub(addUserToClubDto, User);
            if (result.Members.Any(member => member == User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value))
                return Ok(result);
            return BadRequest(ModelState);
        }
    }
}
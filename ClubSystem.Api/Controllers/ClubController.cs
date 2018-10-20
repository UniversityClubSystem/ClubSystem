using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Model.Club;
using Microsoft.AspNetCore.Mvc;

namespace ClubSystem.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        public ClubController(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }

        [HttpGet("getAllClubs")]
        public IActionResult GetAllClubs()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var users = _clubRepository.GetAllClubs();

            return Ok(users);
        }

        [HttpGet("getClub/{id}")]
        public IActionResult GetClub(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _clubRepository.GetClub(id);

            return Ok(user);
        }

        [HttpPost("addClub")]
        public IActionResult AddClub([FromBody] Club club)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var id =_clubRepository.AddClub(club);
            
            return Ok(id);
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Model.User;
using Microsoft.AspNetCore.Mvc;

namespace ClubSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IClubRepository _clubRepository;
        public UserController(IUserRepository userRepository, IClubRepository clubRepository)
        {
            _userRepository = userRepository;
            _clubRepository = clubRepository;
        }

        [HttpGet("getAllUsers", Name = "AllUsers")]
        public IActionResult GetAllUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = _userRepository.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("getUser/{id}", Name = "User")]
        public IActionResult GetUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUser(id);

            return Ok(user);
        }
        
        [HttpGet("getAllClubs", Name = "AllClubs")]
        public IActionResult GetAllClubs()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = _clubRepository.GetAllClubs();

            return Ok(users);
        }

        [HttpGet("getClub/{id}", Name = "Club")]
        public IActionResult GetClub(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _clubRepository.GetClub(id);

            return Ok(user);
        }

        [HttpPost("addUser", Name = "AddUser")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userRepository.AddUser(user);

            return Ok();
        }

        [HttpPost("getUsersByClub/{clubId}", Name = "GetUsersByClub")]
        public ActionResult<User> GetUsersByClub(int clubId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = _userRepository.GetAllUsersByClub(clubId);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }
    }
}
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Model.User;
using Microsoft.AspNetCore.Mvc;

namespace ClubSystem.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("getAllUsers")]
        public IActionResult GetAllUsers()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var users = _userRepository.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("getUser/{id}")]
        public IActionResult GetUser(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _userRepository.GetUser(id);

            return Ok(user);
        }

        [HttpPost("addUser")]
        public IActionResult AddUser([FromBody] User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = _userRepository.AddUser(user);

            return Ok(response);
        }

        [HttpPost("getUsersByClub/{clubId}")]
        public ActionResult<User> GetUsersByClub(int clubId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var users = _userRepository.GetAllUsersByClub(clubId);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }
    }
}
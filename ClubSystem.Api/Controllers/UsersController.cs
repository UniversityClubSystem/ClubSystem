using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Model.User;
using Microsoft.AspNetCore.Mvc;

namespace ClubSystem.Api.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = _userRepository.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUser(id);

            return Ok(user);
        }

        [HttpPost("/addUser")]
        public IActionResult AddUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userRepository.AddUser(user);

            return Ok();
        }
    }
}
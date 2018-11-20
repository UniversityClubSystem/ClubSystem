using System.Threading.Tasks;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Model;
using ClubSystem.Lib.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClubSystem.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //var users = _userRepository.GetAllUsers();
            //return Ok(users);
            return Ok(_userManager.Users);
        }

        /*
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _userRepository.GetUser(id);

            return Ok(user);
        }
        */

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var loginUser = new ApplicationUser
            {
                Email = user.Email,
                PasswordHash = user.Password
            };

            var result =
                await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.PasswordHash, false, false);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newUser = new ApplicationUser {UserName = user.Name, Email = user.Email};
            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (result.Succeeded) return Ok(result);

            foreach (var responseError in result.Errors)
            {
                ModelState.AddModelError("errors", responseError.Description);
            }

            return BadRequest(ModelState);
        }
    }
}
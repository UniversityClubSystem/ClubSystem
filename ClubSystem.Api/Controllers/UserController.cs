using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ClubSystem.Lib.Model;
using ClubSystem.Lib.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ClubSystem.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpGet, Authorize]
        public IActionResult GetAllUsers()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(_userManager.Users.ToList());
        }

        [HttpPost("login")]
        public async Task<object> Login([FromBody] User user) // TODO: LoginDto model
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, false, false);

            if (!result.Succeeded) throw new ApplicationException("INVALID_LOGIN_ATTEMPT");

            var appUser = _userManager.Users.SingleOrDefault(r => r.Email == user.Email);
            return GenerateJwtToken(user.Email, appUser);
        }

        [HttpPost]
        public async Task<object> Register([FromBody] User user) // TODO: RegisterDto model
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newUser = new ApplicationUser
            {
                UserName = user.Username, Email = user.Email
            };
            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, false);
                return GenerateJwtToken(user.Email, newUser);
            }

            foreach (var responseError in result.Errors)
            {
                ModelState.AddModelError("errors", responseError.Description);
            }

            return BadRequest(ModelState);
        }

        // TODO: Extension metoda çevirilebilir
        private string GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
using ClubSystem.Api.Controllers;
using ClubSystem.Lib.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using ClubSystem.Lib.Model.User;
using Microsoft.Extensions.Options;
using System;
using ClubSystem.Api.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace ClubSystem.Test
{
    public class UserControllerTests
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly UserController _userController;

        public UserControllerTests(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            
            _jwtTokenGenerator = new JwtTokenGenerator(configuration);
            _userController = new UserController(userManager, signInManager, configuration);
        }

        [Fact]
        public async void UserCanLogin()
        {
            // Arrange
            var mockUser = new User { UserName = "user1", Email = "mail@mail.com", Password = "Ultra_Secure2Password!" };
            
            // Act
            var result = await _userController.Login(mockUser);

            // Assert
            Assert.IsType("".GetType(), result.GetType());
        }
    }

//    public class FakeUserManager : UserManager<ApplicationUser>
//    {
//        public FakeUserManager() : base(new Mock<IUserStore<ApplicationUser>>().Object,
//                  new Mock<IOptions<IdentityOptions>>().Object,
//                  new Mock<IPasswordHasher<ApplicationUser>>().Object,
//                  new IUserValidator<ApplicationUser>[0],
//                  new IPasswordValidator<ApplicationUser>[0],
//                  new Mock<ILookupNormalizer>().Object,
//                  new Mock<IdentityErrorDescriber>().Object,
//                  new Mock<IServiceProvider>().Object,
//                  new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
//        {
//
//        }
//    }

//    public class FakeSignInManager : SignInManager<ApplicationUser>
//    {
//        public FakeSignInManager() : base(new FakeUserManager(),
//            new Mock<IHttpContextAccessor>().Object,
//            new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
//            new Mock<IOptions<IdentityOptions>>().Object,
//            new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
//            new Mock<IAuthenticationSchemeProvider>().Object)
//        {
//
//        }
//    }
}

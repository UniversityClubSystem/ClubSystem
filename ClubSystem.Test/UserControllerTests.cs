using ClubSystem.Api.Controllers;
using ClubSystem.Lib.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using ClubSystem.Lib.Model.User;
using Microsoft.Extensions.Options;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace ClubSystem.Test
{
    public class UserControllerTests
    {
        private Mock<FakeUserManager> _mockUserManager;
        private Mock<FakeSignInManager> _mockSignInManager;
        private UserController _userController;

        public UserControllerTests()
        {
            _mockUserManager = new Mock<FakeUserManager>();
            _mockSignInManager = new Mock<FakeSignInManager>();
        }

        [Fact]
        public async void UserCanLogin()
        {
            // Arrange
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>(); // ApplicationUser => User

            _mockUserManager = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object,
                null, null, null, null, null, null, null, null);

            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();

            _mockSignInManager = new Mock<SignInManager<ApplicationUser>>(_mockUserManager.Object,
                contextAccessor.Object, userPrincipalFactory.Object, null, null, null);


            var mockConfiguration = new Mock<IConfiguration>();
            var mockUser = new User
            {
                UserName = "user1",
                Email = "mail@mail.com",
                Password = "Ultra_Secure2Password!"
            };
            
            // Act
            var result = await _userController.Login(mockUser);

            // Assert
            Assert.IsType("".GetType(), result.GetType());
        }
    }

    public class FakeUserManager : UserManager<ApplicationUser>
    {
        public FakeUserManager() : base(new Mock<IUserStore<ApplicationUser>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<ApplicationUser>>().Object,
                  new IUserValidator<ApplicationUser>[0],
                  new IPasswordValidator<ApplicationUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
        {

        }
    }

    public class FakeSignInManager : SignInManager<ApplicationUser>
    {
        public FakeSignInManager() : base(new FakeUserManager(),
            new Mock<IHttpContextAccessor>().Object,
            new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
            new Mock<IAuthenticationSchemeProvider>().Object)
        {

        }
    }
}

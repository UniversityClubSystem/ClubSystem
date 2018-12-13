using Xunit;
using ClubSystem.Lib.Models.Entities;

namespace ClubSystem.Test
{
    public class UserControllerTests
    {
        [Fact]
        public void UserCanLogin()
        {
            // Arrange
            var mockUser = new User { UserName = "user1", Email = "mail@mail.com", Password = "Ultra_Secure2Password!" };
            
            // Act
            // var result = await _userController.Login(mockUser);

            // Assert
            // Assert.IsType("".GetType(), result.GetType());
        }
    }
}

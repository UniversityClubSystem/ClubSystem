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

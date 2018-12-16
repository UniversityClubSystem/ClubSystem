using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ClubSystem.Lib;
using ClubSystem.Lib.Exceptions;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models.Entities;
using ClubSystem.Lib.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClubSystem.Test
{
    public class UserRepositoryTest
    {
        //[Fact]
        //public void ShouldThrowExceptionWhenEmptyUser()
        //{
        //    IUserRepository userRepository = GetInMemoryUserRepository();
        //    User user = new User();

        //    Assert.Throws<UserNameCannotBeNullException>(() => userRepository.AddUser(user));
        //}

        //[Fact]
        //public void ShouldThrowNullReferenceException()
        //{
        //    IUserRepository userRepository = GetInMemoryUserRepository();

        //    User user = null;

        //    Assert.Throws<UserCannotBeNullException>(() => userRepository.AddUser(user));
        //}

        //[Fact]
        //public void ShouldThrowNameCannotBeNullException()
        //{
        //    IUserRepository userRepository = GetInMemoryUserRepository();

        //    User user = new User { UserName = null };

        //    Assert.Throws<UserNameCannotBeNullException>(() => userRepository.AddUser(user));
        //}

        //[Fact]
        //public void ShouldGetAllAddedUsers()
        //{
        //    IUserRepository userRepository = GetInMemoryUserRepository();

        //    userRepository.AddUsers(new Collection<User> {
        //        new User { UserName = "user1" },
        //        new User { UserName = "user2" },
        //        new User { UserName = "user3" }
        //    });

        //    IEnumerable<User> users = userRepository.GetAllUsers();

        //    Assert.Collection(users,
        //        item => Assert.Contains("user1", item.UserName),
        //        item => Assert.Contains("user2", item.UserName),
        //        item => Assert.Contains("user3", item.UserName));
        //}

        //private IUserRepository GetInMemoryUserRepository()
        //{
        //    DbContextOptions<ClubSystemDbContext> options;
        //    var builder = new DbContextOptionsBuilder<ClubSystemDbContext>();
        //    options = builder.UseInMemoryDatabase(new Guid().ToString()).Options;

        //    ClubSystemDbContext clubSystemDbContext = new ClubSystemDbContext(options);
        //    clubSystemDbContext.Database.EnsureDeleted();
        //    clubSystemDbContext.Database.EnsureCreated();
        //    return new UserRepository(clubSystemDbContext);
        //}
    }
}

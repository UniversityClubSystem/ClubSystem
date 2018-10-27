using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using ClubSystem.Lib;
using ClubSystem.Lib.Exceptions;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Model.User;
using ClubSystem.Lib.Repository;
using Xunit;

namespace ClubSystem.Test
{
    public class UserRepositoryTest
    {
        [Fact]
        public void Should_Throw_Exception_When_Empty_User()
        {
            IUserRepository userRepository = GetInMemoryUserRepository();

            User user = new User();

            Assert.Throws<UserNameCannotBeNullException>(() => userRepository.AddUser(user));
        }

        [Fact]
        public void Should_Throw_Null_Reference_Exception()
        {
            IUserRepository userRepository = GetInMemoryUserRepository();

            User user = null;

            Assert.Throws<UserCannotBeNullException>(() => userRepository.AddUser(user));
        }

        [Fact]
        public void Should_Throw_Name_Cannot_Be_Null_Exception()
        {
            IUserRepository userRepository = GetInMemoryUserRepository();

            User user = new User { Name = null };

            Assert.Throws<UserNameCannotBeNullException>(() => userRepository.AddUser(user));
        }

        [Fact]
        public void Should_Get_All_Added_Users()
        {
            IUserRepository userRepository = GetInMemoryUserRepository();

            userRepository.AddUsers(new Collection<User> {
                new User { Name = "user1" },
                new User { Name = "user2" },
                new User { Name = "user3" }
            });

            IEnumerable<User> users = userRepository.GetAllUsers();

            Assert.Collection(users,
                item => Assert.Contains("user1", item.Name),
                item => Assert.Contains("user2", item.Name),
                item => Assert.Contains("user3", item.Name));
        }

        private IUserRepository GetInMemoryUserRepository()
        {
            DbContextOptions<ClubSystemDbContext> options;
            var builder = new DbContextOptionsBuilder<ClubSystemDbContext>();
            options = builder.UseInMemoryDatabase("InMemoryClubSystemDb").Options;

            ClubSystemDbContext clubSystemDbContext = new ClubSystemDbContext(options);
            clubSystemDbContext.Database.EnsureDeleted();
            clubSystemDbContext.Database.EnsureCreated();
            return new UserRepository(clubSystemDbContext);
        }
    }
}

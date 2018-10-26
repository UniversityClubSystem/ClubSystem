using ClubSystem.Lib;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Model.User;
using ClubSystem.Lib.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClubSystem.Test
{
    public class UserRepositoryTest
    {
        [Fact]
        public void Not_Add_When_No_Name()
        {
            IUserRepository userRepository = GetInMemoryUserRepository();
            User user = new User();

            User addedUser = userRepository.AddUser(user);

            Assert.Single(userRepository.GetAll());
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

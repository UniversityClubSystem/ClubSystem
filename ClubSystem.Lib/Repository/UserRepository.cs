using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Model;
using ClubSystem.Lib.Model.Club;
using ClubSystem.Lib.Model.User;
using Microsoft.EntityFrameworkCore;

namespace ClubSystem.Lib.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ClubSystemDbContext _context;

        public UserRepository(ClubSystemDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            var club = new Club
            {
                Name = "SpaceClub", CreatedDate = DateTime.Now,
                UniversityName = "London University"
            };
            _context.Clubs.Add(club);

            var _user = new User
            {
                Name = user.Name,
                CreatedDate = DateTime.Now,
                UserClubs = new List<UserClub>()
            };
            _context.Users.Add(_user);

            var userClub = new UserClub
            {
                User = _user,
                Club = club
            };
            _user.UserClubs.Add(userClub);
            
            await _context.SaveChangesAsync();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Set<User>().ToList();
        }

        public IEnumerable<User> GetAllUsersByClub(int clubId)
        {
            var users = _context.Users
                .Include(user => user.UserClubs)
                .ToList();

            return users
                .Where(user => user.UserClubs
                    .Any(userClubs => userClubs.ClubId == clubId))
                .ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.SingleOrDefault(user => user.Id == id);
        }
    }
}
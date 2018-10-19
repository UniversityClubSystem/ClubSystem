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

        public int AddUser(User user)
        {
            var newUser = new User
            {
                Name = user.Name,
                CreatedDate = DateTime.Now,
                UserClubs = new List<UserClub>()
            };

            foreach (var userClub in user.UserClubs)
            {
                newUser.UserClubs.Add(userClub);
            }
            
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser.Id;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Set<User>()
                .Include(user => user.UserClubs)
                .ToList();
        }
        
        public User GetUser(int id)
        {
            return _context.Users
                .SingleOrDefault(user => user.Id == id);
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
    }
}
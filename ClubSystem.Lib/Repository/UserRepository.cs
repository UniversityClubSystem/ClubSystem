using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ClubSystem.Lib.Exceptions;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Model;
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

        public User AddUser(User user)
        {
            ValidateUser(user);

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
            return newUser;
        }

        /// <summary>
        /// Bu metod user nesnesinin null kontrollerini yapar ve gerekli hatalarý verir.
        /// </summary>
        /// <param name="user"></param>
        private void ValidateUser(User user)
        {
            if (user == null)
            {
                throw new UserCannotBeNullException("User Cannot Be Null");
            }
            else if (user.Name == null)
            {
                throw new UserNameCannotBeNullException("Username Cannot Be Null");
            }
        }

        public IEnumerable<User> AddUsers(IEnumerable<User> users)
        {
            var newUsers = new Collection<User>();

            foreach (var user in users)
            {
                var newUser = AddUser(user);

                if (newUser != null)
                {
                    newUsers.Add(newUser);
                }
            }

            Context.AddRange(newUsers);

            return newUsers;
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
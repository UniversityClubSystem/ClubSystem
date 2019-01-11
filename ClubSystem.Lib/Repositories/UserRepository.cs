using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ClubSystem.Lib.Exceptions;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models;
using Microsoft.EntityFrameworkCore;

namespace ClubSystem.Lib.Repositories
{
    public class UserRepository : Repository<User>, IApplicationUserRepository
    {
        private readonly ClubSystemDbContext _context;

        public UserRepository(ClubSystemDbContext context) : base(context)
        {
            _context = context;
        }

        public User AddApplicationUser(User ApplicationUser)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> AddApplicationUsers(IEnumerable<User> ApplicationUsers)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllApplicationUsers()
        {
            throw new NotImplementedException();
        }

        public User GetApplicationUser(int id)
        {
            throw new NotImplementedException();
        }

        //public ApplicationUser AddUser(ApplicationUser user)
        //{
        //    ValidateUser(user);

        //    var newUser = new ApplicationUser
        //    {
        //        UserName = user.UserName,
        //        CreatedDate = DateTime.Now,
        //        UserClubs = new List<UserClub>()
        //    };

        //    foreach (var userClub in user.UserClubs)
        //    {
        //        newUser.UserClubs.Add(userClub);
        //    }

        //    _context.Users.Add(newUser);
        //    _context.SaveChanges();
        //    return newUser;
        //}

        ///// <summary>
        ///// Bu metod user nesnesinin null kontrollerini yapar ve gerekli hatalar� verir.
        ///// </summary>
        ///// <param name="user"></param>
        //private void ValidateUser(ApplicationUser user)
        //{
        //    if (user == null)
        //    {
        //        throw new UserCannotBeNullException("User Cannot Be Null");
        //    }

        //    if (user.UserName == null)
        //    {
        //        throw new UserNameCannotBeNullException("Username Cannot Be Null");
        //    }
        //}

        //public IEnumerable<ApplicationUser> AddUsers(IEnumerable<ApplicationUser> users)
        //{
        //    var newUsers = new Collection<ApplicationUser>();

        //    foreach (var user in users)
        //    {
        //        var newUser = AddUser(user);

        //        if (newUser != null)
        //        {
        //            newUsers.Add(newUser);
        //        }
        //    }

        //    Context.AddRange(newUsers);

        //    return newUsers;
        //}

        //public IEnumerable<ApplicationUser> GetAllUsers()
        //{
        //    return _context.Set<ApplicationUser>()
        //        .Include(user => user.UserClubs)
        //        .ToList();
        //}

        //public ApplicationUser GetUser(int id)
        //{
        //    return _context.ApplicationUser
        //        .SingleOrDefault(user => user.Id == id);
        //}

        //public IEnumerable<ApplicationUser> GetAllUsersByClub(int clubId)
        //{
        //    var users = _context.ApplicationUser
        //        .Include(user => user.UserClubs)
        //        .ToList();

        //    return users
        //        .Where(user => user.UserClubs
        //            .Any(userClubs => userClubs.ClubId == clubId))
        //        .ToList();
        //}
    }
}
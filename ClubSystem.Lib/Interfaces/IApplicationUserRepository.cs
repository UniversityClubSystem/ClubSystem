using System.Collections.Generic;
using ClubSystem.Lib.Models;
using ClubSystem.Lib.Models.Entities;

namespace ClubSystem.Lib.Interfaces
{
    public interface IApplicationUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllApplicationUsers();
        User GetApplicationUser(int id);
        User AddApplicationUser(User ApplicationUser);
        IEnumerable<User> AddApplicationUsers(IEnumerable<User> ApplicationUsers);
    }
}

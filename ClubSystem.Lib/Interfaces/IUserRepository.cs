using System.Collections.Generic;
using ClubSystem.Lib.Models.Entities;

namespace ClubSystem.Lib.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(int id);
        User AddUser(User user);
        IEnumerable<User> AddUsers(IEnumerable<User> users);
    }
}

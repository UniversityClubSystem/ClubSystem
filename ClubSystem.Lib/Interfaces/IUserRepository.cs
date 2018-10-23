using ClubSystem.Lib.Model.User;
using System.Collections.Generic;

namespace ClubSystem.Lib.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(int id);
        int AddUser(User user);
    }
}

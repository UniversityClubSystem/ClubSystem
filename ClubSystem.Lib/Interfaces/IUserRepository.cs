using ClubSystem.Lib.Model.User;
using System.Collections.Generic;

namespace ClubSystem.Lib.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetAllUsersByClub(int clubId);
        User GetUser(int id);
        void AddUser(User user);
    }
}

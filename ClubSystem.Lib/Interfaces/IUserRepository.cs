using ClubSystem.Lib.Model.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubSystem.Lib.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetAllUsersByClub(int clubId);
        User GetUser(int id);
        User AddUser(User user);
        IEnumerable<User> AddUsers(IEnumerable<User> users);
    }
}

using ClubSystem.Lib.Model.User;
using System.Collections.Generic;

namespace ClubSystem.Lib.Interfaces
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        IEnumerable<UserEntity> GetAllUsers();
        IEnumerable<UserEntity> GetAllUsersByClub(int clubId);
        UserEntity GetUser(int id);
    }
}

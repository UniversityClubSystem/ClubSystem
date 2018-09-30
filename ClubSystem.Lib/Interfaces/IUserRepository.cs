using ClubSystem.Lib.Model.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubSystem.Lib.Interfaces
{
    interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetAllUsersByClub();
        User GetUser(int id);
    }
}

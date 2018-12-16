using System.Collections.Generic;
using ClubSystem.Lib.Models;
using ClubSystem.Lib.Models.Entities;

namespace ClubSystem.Lib.Interfaces
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        IEnumerable<ApplicationUser> GetAllApplicationUsers();
        ApplicationUser GetApplicationUser(int id);
        ApplicationUser AddApplicationUser(ApplicationUser ApplicationUser);
        IEnumerable<ApplicationUser> AddApplicationUsers(IEnumerable<ApplicationUser> ApplicationUsers);
    }
}

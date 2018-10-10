using System.Collections.Generic;
using System.Threading.Tasks;
using ClubSystem.Lib.Model.Club;
using ClubSystem.Lib.Model.User;

namespace ClubSystem.Lib.Interfaces
{
    public interface IClubRepository : IRepository<Club>
    {
        IEnumerable<Club> GetClub(int id);
        IEnumerable<Club> GetAllClubs();
        IEnumerable<Club> GetAllClubsByUser(int id);
        Task AddClub(User user);
    }
}

using System.Collections.Generic;
using ClubSystem.Lib.Model.Club;

namespace ClubSystem.Lib.Interfaces
{
    public interface IClubRepository : IRepository<Club>
    {
        IEnumerable<Club> GetClub(int id);
        IEnumerable<Club> GetAllClubs();
        IEnumerable<Club> GetAllClubsByUser(int id);
        int AddClub(Club club);
    }
}

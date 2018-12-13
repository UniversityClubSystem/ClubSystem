using System.Collections.Generic;
using ClubSystem.Lib.Model.Club;

namespace ClubSystem.Lib.Interfaces
{
    public interface IClubRepository : IRepository<Club>
    {
        IEnumerable<Club> GetAllClubs();
        Club GetClub(int id);
        Club AddClub(Club club);
    }
}

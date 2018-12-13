using System.Collections.Generic;
using ClubSystem.Lib.Models.Entities;

namespace ClubSystem.Lib.Interfaces
{
    public interface IClubRepository : IRepository<Club>
    {
        IEnumerable<Club> GetAllClubs();
        Club GetClub(int id);
        Club AddClub(Club club);
    }
}

using System.Collections.Generic;
using ClubSystem.Lib.Models.Entities;

namespace ClubSystem.Lib.Interfaces
{
    public interface IClubRepository : IRepository<Club>
    {
        IEnumerable<Club> GetAllClubs();
        Club GetClub(string id);
        Club AddClub(Club club);
    }
}

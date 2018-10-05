using ClubSystem.Lib.Model.Club;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubSystem.Lib.Interfaces
{
    public interface IClubRepository : IRepository<Club>
    {
        IEnumerable<Club> GetAllClubs();
        IEnumerable<Club> GetAllClubsByUser();
    }
}

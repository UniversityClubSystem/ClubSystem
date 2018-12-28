using System.Collections.Generic;
using ClubSystem.Lib.Models.Dtos;
using ClubSystem.Lib.Models.Entities;
using ClubSystem.Lib.Models.Resources;

namespace ClubSystem.Lib.Interfaces
{
    public interface IClubRepository : IRepository<Club>
    {
        IEnumerable<ClubResource> GetAllClubs();
        ClubResource GetClub(string id);
        ClubResource AddClub(ClubDto clubDto);
    }
}

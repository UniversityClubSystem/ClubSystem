using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
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
        IEnumerable<ClubResource> GetClubsByUser(string id);
        IEnumerable<ClubResource> GetClubsByCurrentUser(ClaimsPrincipal claimsPrincipal);
        Task<ClubResource> AddUserToClub(AddUserToClubDto addUserToClubDto);
    }
}
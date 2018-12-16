using System;
using System.Collections.Generic;
using System.Linq;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models.Entities;

namespace ClubSystem.Lib.Repository
{
    public class ClubRepository : Repository<Club>, IClubRepository
    {
        private readonly ClubSystemDbContext _context;
        public ClubRepository(ClubSystemDbContext context) : base(context)
        {
            _context = context;
        }

        public Club AddClub(Club club)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Club> GetAllClubs()
        {
            throw new NotImplementedException();
        }

        public Club GetClub(int id)
        {
            throw new NotImplementedException();
        }

        //public Club AddClub(Club club)
        //{
        //    var newClub = new Club
        //    {
        //        Name = club.Name,
        //        UniversityName = club.UniversityName,
        //        CreatedDate = DateTime.Now,
        //        UserClubs = new List<UserClub>()
        //    };

        //    foreach (var userClub in club.UserClubs)
        //    {
        //        newClub.UserClubs.Add(userClub);
        //    }

        //    _context.Clubs.Add(newClub);
        //    _context.SaveChanges();
        //    return newClub;
        //}

        //public IEnumerable<Club> GetAllClubs()
        //{
        //    return _context.Set<Club>().ToList();
        //}

        //public Club GetClub(int id)
        //{
        //    return _context.Clubs.SingleOrDefault(club => club.Id == id);
        //}
    }
}

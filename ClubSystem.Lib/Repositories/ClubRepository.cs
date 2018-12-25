using System;
using System.Collections.Generic;
using System.Linq;
using ClubSystem.Lib.Exceptions;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models.Entities;
using ClubSystem.Lib.Validators;

namespace ClubSystem.Lib.Repositories
{
    public class ClubRepository : Repository<Club>, IClubRepository
    {
        private readonly ClubSystemDbContext _context;
        
        public ClubRepository(ClubSystemDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Club> GetAllClubs()
        {
            return _context.Set<Club>().ToList();
        }

        public Club GetClub(string id)
        {
            return _context.Clubs.SingleOrDefault(club => club.Id == id);
        }

        public Club AddClub(Club club)
        {
            if (club == null)
            {
                throw new ClubCannotBeNullException();
            }

            var clubValidator = new ClubValidator();
            var validationResult = clubValidator.Validate(club);

            if (!validationResult.IsValid) throw new ClubIsNotValidException(validationResult.Errors.First().ErrorMessage);
            var newClub = new Club
            {
                Name = club.Name,
                UniversityName = club.UniversityName,
                CreatedDate = DateTime.Now,
                UserClubs = new List<UserClub>()
            };

            foreach (var userClub in club.UserClubs)
            {
                newClub.UserClubs.Add(userClub);
            }

            _context.Clubs.Add(newClub);
            _context.SaveChanges();
            return newClub;
        }
    }
}

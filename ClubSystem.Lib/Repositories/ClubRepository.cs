using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ClubSystem.Lib.Exceptions;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.MapProfiles;
using ClubSystem.Lib.Models.Dtos;
using ClubSystem.Lib.Models.Entities;
using ClubSystem.Lib.Models.Resources;
using ClubSystem.Lib.Validators;

namespace ClubSystem.Lib.Repositories
{
    public class ClubRepository : Repository<Club>, IClubRepository
    {
        private readonly ClubSystemDbContext _context;
        private readonly IMapper _mapper;
        
        public ClubRepository(ClubSystemDbContext context) : base(context)
        {
            _context = context;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ClubProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public IEnumerable<ClubResource> GetAllClubs()
        {
            var clubs = _context.Set<Club>().ToList();
            return clubs.Select(club => _mapper.Map<ClubResource>(club)).ToList();
        }

        public ClubResource GetClub(string id)
        {
            var club = _context.Clubs.SingleOrDefault(x => x.Id == id);
            var clubResource = _mapper.Map<ClubResource>(club);
            return clubResource;
        }

        public ClubResource AddClub(ClubDto clubDto)
        {
            #region Checks

            if (clubDto == null)
            {
                throw new ClubCannotBeNullException();
            }

            var clubValidator = new ClubValidator();
            var validationResult = clubValidator.Validate(clubDto);

            if (!validationResult.IsValid) throw new ClubIsNotValidException(validationResult.Errors.First().ErrorMessage);

            #endregion

            var newClub = _mapper.Map<Club>(clubDto);
           
            _context.Clubs.Add(newClub);
            _context.SaveChanges();

            var clubResource = _mapper.Map<ClubResource>(newClub);
            return clubResource;
        }
    }
}

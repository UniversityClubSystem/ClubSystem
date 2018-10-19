﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Model;
using ClubSystem.Lib.Model.Club;
using ClubSystem.Lib.Model.User;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ClubSystem.Lib.Repository
{
    public class ClubRepository : Repository<Club>, IClubRepository
    {
        private readonly ClubSystemDbContext _context;
        public ClubRepository(ClubSystemDbContext context) : base(context)
        {
            _context = context;
        }
        
        public int AddClub(Club club)
        {
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
            return newClub.Id;
        }
        
        public IEnumerable<Club> GetAllClubs()
        {
            return _context.Set<Club>().ToList();
        }

        public IEnumerable<Club> GetClub(int id)
        {
            return _context.Clubs.Where(club => club.Id == id);
        }
        
        public IEnumerable<Club> GetAllClubsByUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
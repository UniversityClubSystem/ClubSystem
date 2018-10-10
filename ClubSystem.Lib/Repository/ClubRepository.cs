using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Model.Club;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClubSystem.Lib.Model.User;

namespace ClubSystem.Lib.Repository
{
    public class ClubRepository : Repository<Club>, IClubRepository
    {
        private readonly ClubSystemDbContext _context;
        public ClubRepository(ClubSystemDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Club> GetClub(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Club> GetAllClubs()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Club> GetAllClubsByUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddClub(User user)
        {
            throw new NotImplementedException();
        }
    }
}

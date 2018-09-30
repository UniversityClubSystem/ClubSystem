using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Model.Club;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubSystem.Lib.Repository
{
    public class ClubRepository : Repository<Club>, IClubRepository
    {
        public ClubRepository(ClubSystemDbContext context) : base(context)
        {
        }

        public IEnumerable<Club> GetAllClubs()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Club> GetAllClubsByUser()
        {
            throw new NotImplementedException();
        }
    }
}

using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Model.Club;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubSystem.Lib.Repository
{
    public class ClubRepository : Repository<ClubEntity>, IClubRepository
    {
        public ClubRepository(ClubSystemDbContext context) : base(context)
        {
        }

        public IEnumerable<ClubEntity> GetAllClubs()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClubEntity> GetAllClubsByUser()
        {
            throw new NotImplementedException();
        }
    }
}

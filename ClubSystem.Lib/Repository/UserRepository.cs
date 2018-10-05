using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Model.User;

namespace ClubSystem.Lib.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ClubSystemDbContext _context;
        
        public UserRepository(ClubSystemDbContext context) : base(context)
        {
            _context = context;
        }
        
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Set<User>().ToList();
        }

        public IEnumerable<User> GetAllUsersByClub(int clubId)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
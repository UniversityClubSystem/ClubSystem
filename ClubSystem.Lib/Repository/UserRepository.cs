using System;
using System.Collections.Generic;
using System.Linq;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Model.User;

namespace ClubSystem.Lib.Repository
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        private readonly ClubSystemDbContext _context;
        
        public UserRepository(ClubSystemDbContext context) : base(context)
        {
            _context = context;
        }
        
        public IEnumerable<UserEntity> GetAllUsers()
        {
            return _context.Set<UserEntity>().ToList();
        }

        public IEnumerable<UserEntity> GetAllUsersByClub(int clubId)
        {
            throw new NotImplementedException();
        }

        public UserEntity GetUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
using ClubSystem.Lib.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubSystem.Lib.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ClubSystemDbContext _context;
        public Repository(ClubSystemDbContext context)
        {
            _context = context;
        }

        protected void Save() => _context.SaveChanges();
        public int Count(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).Count();
        }

        public void Create(T entity)
        {
            _context.Add(entity);
            Save();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            Save();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T entitiy)
        {
            _context.Entry(entitiy).State = EntityState.Modified;
            Save();
        }
    }
}

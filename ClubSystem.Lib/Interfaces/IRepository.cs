using System;
using System.Collections.Generic;

namespace ClubSystem.Lib.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> Find(Func<T, bool> predicate);

        T GetById(int id);

        void Create(T entity);

        void Update(T entitiy);

        void Delete(T entitiy);

        int Count(Func<T, bool> predicate);
    }
}

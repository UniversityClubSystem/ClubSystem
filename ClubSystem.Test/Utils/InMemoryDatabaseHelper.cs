using ClubSystem.Lib;
using ClubSystem.Lib.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClubSystem.Test.Utils
{
    public class InMemoryDatabaseHelper<T> where T : class
    {
        // TODO: make generic repository provider with InMemoryDatabase
        public Repository<T> GetRepositoryWithInMemoryDatabase()
        {
            DbContextOptions<ClubSystemDbContext> options;
            var builder = new DbContextOptionsBuilder<ClubSystemDbContext>();
            options = builder.UseInMemoryDatabase(new Guid().ToString()).Options;

            ClubSystemDbContext clubSystemDbContext = new ClubSystemDbContext(options);
            clubSystemDbContext.Database.EnsureDeleted();
            clubSystemDbContext.Database.EnsureCreated();
            return new Repository<T>(clubSystemDbContext);
        }
    }
}

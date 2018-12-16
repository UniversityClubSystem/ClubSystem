using ClubSystem.Lib.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubSystem.Lib
{
    public class ClubSystemDbContext : DbContext
    {
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public ClubSystemDbContext(DbContextOptions<ClubSystemDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
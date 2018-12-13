using ClubSystem.Lib.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubSystem.Lib
{
    public class ClubSystemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public ClubSystemDbContext(DbContextOptions<ClubSystemDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserClub>().HasKey(uc =>
                new {uc.UserId, uc.ClubId});

            modelBuilder.Entity<UserPost>().HasKey(up =>
                new {up.UserId, up.PostId});
        }
    }
}
using ClubSystem.Lib.Model;
using ClubSystem.Lib.Model.Club;
using ClubSystem.Lib.Model.User;
using Microsoft.EntityFrameworkCore;

namespace ClubSystem.Lib
{
    public class ClubSystemDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ClubEntity> Clubs { get; set; }
        
        public ClubSystemDbContext(DbContextOptions<ClubSystemDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserClubEntity>()
                .HasKey(uc => new {uc.UserId, uc.ClubId});

            modelBuilder.Entity<UserClubEntity>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserClubEntities)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserClubEntity>()
                .HasOne(uc => uc.Club)
                .WithMany(c => c.UserClubEntities)
                .HasForeignKey(uc => uc.ClubId);
        }
    }
}

using ClubSystem.Lib.Models;
using ClubSystem.Lib.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClubSystem.Lib
{
    public class ClubSystemDbContext : IdentityDbContext<User>
    {
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public ClubSystemDbContext(DbContextOptions<ClubSystemDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserPost>()
                .HasKey(up => new {up.UserId, up.PostId});

            modelBuilder.Entity<UserPost>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPosts)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserPost>()
                .HasOne(up => up.Post)
                .WithMany(u => u.UserPosts)
                .HasForeignKey(up => up.PostId);

            modelBuilder.Entity<UserClub>()
                .HasKey(up => new {up.UserId, up.ClubId});

            modelBuilder.Entity<UserClub>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserClubs)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserClub>()
                .HasOne(up => up.Club)
                .WithMany(u => u.UserClubs)
                .HasForeignKey(up => up.ClubId);

            modelBuilder.Entity<ClubPost>()
                .HasKey(cp => new {cp.ClubId, cp.PostId});

            modelBuilder.Entity<ClubPost>()
                .HasOne(cp => cp.Club)
                .WithMany(c => c.ClubPosts)
                .HasForeignKey(cp => cp.ClubId);

            modelBuilder.Entity<ClubPost>()
                .HasOne(cp => cp.Post)
                .WithMany(c => c.ClubPosts)
                .HasForeignKey(cp => cp.PostId);
        }
    }
}
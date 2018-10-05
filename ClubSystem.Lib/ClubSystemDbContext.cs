using System;
using System.Collections.Generic;
using ClubSystem.Lib.Model;
using ClubSystem.Lib.Model.Club;
using ClubSystem.Lib.Model.User;
using Microsoft.EntityFrameworkCore;

namespace ClubSystem.Lib
{
    public class ClubSystemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<UserClub> UserClubs { get; set; }

        public ClubSystemDbContext(DbContextOptions<ClubSystemDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserClub>()
                .HasKey(uc => new {uc.UserId, uc.ClubId});
            
            var user1 = new User
            {
                Id = 1,
                Name = "Ömrüm Baki Temiz",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };
            
            var club1 = new Club
            {
                Id = 1,
                Name = "Science Club",
                UniversityName = "London University",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };
            
            var userClub1 = new List<UserClub>
            {
                new UserClub
                {
                    User = user1,
                    Club = club1
                }
            };

            user1.UserClubs = userClub1;
            club1.UserClubs = userClub1;
            
            modelBuilder.Entity<User>(u => u.HasData(user1));
            modelBuilder.Entity<Club>(c => c.HasData(club1));
        }
    }
}
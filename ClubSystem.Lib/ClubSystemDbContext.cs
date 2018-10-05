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

        public ClubSystemDbContext(DbContextOptions<ClubSystemDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserClub>().HasKey(uc =>
                new { uc.UserId, uc.ClubId });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "Ömrüm Baki Temiz",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 2,
                Name = "admin",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            modelBuilder.Entity<Club>().HasData(new Club
            {
                Id = 1,
                Name = "Space Club",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                UniversityName = "London University"
            });
        }
    }
}
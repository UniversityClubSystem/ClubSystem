using System.Collections.Generic;
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
        public DbSet<UserClubEntity> UserClubs { get; set; }

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

//            modelBuilder.Entity<UserEntity>(u =>
//            {
//                u.HasData(new UserEntity
//                {
//                    Id = 1,
//                    Name = "Ömrüm Baki Temiz"
//                });
//
//                u.OwnsOne(e => e.UserClubEntities).HasData();
//            });
//            modelBuilder.Entity<UserEntity>().HasData(
//                new UserEntity
//                {
//                    Id = 1,
//                    Name = "Ömrüm Baki Temiz"
//                },
//                new UserEntity
//                {
//                    Id = 2,
//                    Name = "Steve Jobs"
//                });
//
//            modelBuilder.Entity<ClubEntity>().HasData(
//                new ClubEntity
//                {
//                    Id = 1,
//                    Name = "Computer Club",
//                    UniversityName = "Kocaeli University",
//                    UserClubEntities = new List<UserClubEntity>
//                    {
//                        new UserClubEntity
//                        {
//                            ClubId = 1,
//                            UserId = 1
//                        },
//                        new UserClubEntity
//                        {
//                            ClubId = 1,
//                            UserId = 2
//                        }
//                    }
//                },
//                new ClubEntity
//                {
//                    Id = 2,
//                    Name = "Space Club",
//                    UniversityName = "New York University",
//                    UserClubEntities = new List<UserClubEntity>
//                    {
//                        new UserClubEntity
//                        {
//                            ClubId = 2,
//                            UserId = 2
//                        },
//                        new UserClubEntity
//                        {
//                            ClubId = 2,
//                            UserId = 1
//                        }
//                    }
//                });
        }
    }
}
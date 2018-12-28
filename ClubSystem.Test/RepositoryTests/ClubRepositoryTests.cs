using System;
using System.Collections.Generic;
using System.Linq;
using ClubSystem.Lib;
using ClubSystem.Lib.Exceptions;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models.Dtos;
using ClubSystem.Lib.Models.Entities;
using ClubSystem.Lib.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClubSystem.Test.RepositoryTests
{
    public class ClubRepositoryTests
    {
        [Fact]
        public void ShouldAddClub()
        {
            var clubRepository = GetInMemoryClubRepository();

            var users = new List<UserDto> {new UserDto {UserId = "5"}};
            var club1 = new ClubDto {Name = "Name1", UniversityName = "University1", Users = users};

            var addedClub = clubRepository.AddClub(club1);

            Assert.NotNull(addedClub);
            Assert.Equal(addedClub.Name, club1.Name);
            Assert.Equal(addedClub.UniversityName, club1.UniversityName);
            // Assert.Equal(addedClub.UserClubs, club1.UserClubs);
        }

        [Fact]
        public void ShouldGetOnlyOneClub()
        {
            var clubRepository = GetInMemoryClubRepository();

            var users = new List<UserDto> {new UserDto {UserId = "5"}};
            var club1 = new ClubDto {Name = "Name1", UniversityName = "University1", Users = users};

            clubRepository.AddClub(club1);
            var result = clubRepository.GetAllClubs();

            Assert.Single(result);
        }

        [Fact]
        public void ShouldGetAllClub()
        {
            var clubRepository = GetInMemoryClubRepository();

            var users1 = new List<UserDto> {new UserDto {UserId = "5"}};
            var club1 = new ClubDto {Name = "Name1", UniversityName = "University1", Users = users1};

            var users2 = new List<UserDto> {new UserDto {UserId = "6"}};
            var club2 = new ClubDto {Name = "Name2", UniversityName = "University2", Users = users2};

            clubRepository.AddClub(club1);
            clubRepository.AddClub(club2);
            var result = clubRepository.GetAllClubs();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void ShouldReturnNull()
        {
            var clubRepository = GetInMemoryClubRepository();

            var response = clubRepository.GetClub("1");

            Assert.Null(response);
        }

        [Fact]
        public void ShouldThrowClubIsNotValidException()
        {
            var clubRepository = GetInMemoryClubRepository();
            ClubDto emptyClub = new ClubDto();

            Assert.Throws<ClubCannotBeNullException>(() => clubRepository.AddClub(null));
            Assert.Throws<ClubIsNotValidException>(() => clubRepository.AddClub(emptyClub));
        }

        [Fact]
        public void ShouldReturnClub()
        {
            IClubRepository clubRepository = GetInMemoryClubRepository();

            var users1 = new List<UserDto> {new UserDto {UserId = "5"}};
            var club1 = new ClubDto {Name = "Name1", UniversityName = "University1", Users = users1};

            var users2 = new List<UserDto> {new UserDto {UserId = "6"}};
            var club2 = new ClubDto {Name = "Name2", UniversityName = "University2", Users = users2};

            var addedClub1 = clubRepository.AddClub(club1);
            var addedClub2 = clubRepository.AddClub(club2);
            var response = clubRepository.GetClub(addedClub1.Id);

            Assert.NotEqual(response, addedClub2);
            Assert.Equal(response, addedClub1);
        }

        private IClubRepository GetInMemoryClubRepository()
        {
            DbContextOptions<ClubSystemDbContext> options;
            var builder = new DbContextOptionsBuilder<ClubSystemDbContext>();
            options = builder.UseInMemoryDatabase(new Guid().ToString()).Options;

            ClubSystemDbContext clubSystemDbContext = new ClubSystemDbContext(options);
            clubSystemDbContext.Database.EnsureDeleted();
            clubSystemDbContext.Database.EnsureCreated();
            return new ClubRepository(clubSystemDbContext);
        }
    }
}
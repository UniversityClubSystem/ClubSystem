using System;
using System.Collections.Generic;
using System.Linq;
using ClubSystem.Lib;
using ClubSystem.Lib.Exceptions;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models.Dtos;
using ClubSystem.Lib.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace ClubSystem.Test.RepositoryTests
{
    public class ClubRepositoryTests
    {
        [Fact]
        public void ShouldAddClub()
        {
            var clubRepository = GetInMemoryClubRepository();

            var members = new List<UserDto> {new UserDto {UserId = "5"}};
            var club1 = new ClubDto {Name = "Name1", UniversityName = "University14", Members = members};

            var addedClub = clubRepository.AddClub(club1);

            Assert.NotNull(addedClub);
            Assert.Equal(addedClub.Name, club1.Name);
            Assert.Equal(addedClub.UniversityName, club1.UniversityName);
            foreach (var member in addedClub.Members)
            {
                Assert.Equal(member.Id, club1.Members.Single(club => club.Name == member.Name).UserId);
            }
        }

        [Fact]
        public void ShouldAddAdvancedClub()
        {
            var clubRepository = GetInMemoryClubRepository();

            var members = new List<UserDto> { new UserDto { UserId = "5" , Name = "username1"} };
            var club1 = new ClubDto { Name = "Name1", UniversityName = "University14", Members = members };

            var addedClub = clubRepository.AddClub(club1);

            Assert.NotNull(addedClub);
            Assert.Equal(addedClub.Name, club1.Name);
            Assert.Equal(addedClub.UniversityName, club1.UniversityName);
            // TODO: addedClub.members and club1.members will be compared
        }

        [Fact]
        public void ShouldGetOnlyOneClub()
        {
            var clubRepository = GetInMemoryClubRepository();

            var users = new List<UserDto> {new UserDto {UserId = "5"}};
            var club1 = new ClubDto {Name = "Name1", UniversityName = "University1", Members = users};

            clubRepository.AddClub(club1);
            var result = clubRepository.GetAllClubs();

            Assert.Single(result);
        }

        [Fact]
        public void ShouldGetAllClub()
        {
            var clubRepository = GetInMemoryClubRepository();

            var users1 = new List<UserDto> {new UserDto {UserId = "5"}};
            var club1 = new ClubDto {Name = "Name1", UniversityName = "University1", Members = users1};

            var users2 = new List<UserDto> {new UserDto {UserId = "6"}};
            var club2 = new ClubDto {Name = "Name2", UniversityName = "University2", Members = users2};

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
            var club1 = new ClubDto {Name = "Name1", UniversityName = "University1", Members = users1};

            var users2 = new List<UserDto> {new UserDto {UserId = "6"}};
            var club2 = new ClubDto {Name = "Name2", UniversityName = "University2", Members = users2};

            var addedClub1 = clubRepository.AddClub(club1);
            var addedClub2 = clubRepository.AddClub(club2);
            var response = clubRepository.GetClub(addedClub1.Id);

            Assert.NotEqual(response, addedClub2);
            var responseJson = JsonConvert.SerializeObject(response);
            var addedClub1Json = JsonConvert.SerializeObject(addedClub1);
            Assert.Equal(responseJson, addedClub1Json);
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
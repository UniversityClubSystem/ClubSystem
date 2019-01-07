using System;
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

        [Fact]
        public void ShouldAddAdvancedClub()
        {
            var clubRepository = GetInMemoryClubRepository();

            var club1 = new ClubDto {Name = "Name1", UniversityName = "University14"};

            var addedClub = clubRepository.AddClub(club1);

            Assert.NotNull(addedClub);
            Assert.Equal(addedClub.Name, club1.Name);
            Assert.Equal(addedClub.UniversityName, club1.UniversityName);
            // TODO: addedClub.members and club1.members will be compared
        }

        [Fact]
        public void ShouldAddClub()
        {
            var clubRepository = GetInMemoryClubRepository();

            var club1 = new ClubDto {Name = "Name1", UniversityName = "University14"};

            var addedClub = clubRepository.AddClub(club1);

            Assert.NotNull(addedClub);
            Assert.Equal(addedClub.Name, club1.Name);
            Assert.Equal(addedClub.UniversityName, club1.UniversityName);
        }

        [Fact]
        public void ShouldGetAllClub()
        {
            var clubRepository = GetInMemoryClubRepository();

            var club1 = new ClubDto {Name = "Name1", UniversityName = "University1"};

            var club2 = new ClubDto {Name = "Name2", UniversityName = "University2"};

            clubRepository.AddClub(club1);
            clubRepository.AddClub(club2);
            var result = clubRepository.GetAllClubs();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void ShouldGetOnlyOneClub()
        {
            var clubRepository = GetInMemoryClubRepository();

            var club1 = new ClubDto {Name = "Name1", UniversityName = "University1"};

            clubRepository.AddClub(club1);
            var result = clubRepository.GetAllClubs();

            Assert.Single(result);
        }

        [Fact]
        public void ShouldReturnClub()
        {
            IClubRepository clubRepository = GetInMemoryClubRepository();

            var club1 = new ClubDto {Name = "Name1", UniversityName = "University1"};

            var club2 = new ClubDto {Name = "Name2", UniversityName = "University2"};

            var addedClub1 = clubRepository.AddClub(club1);
            var addedClub2 = clubRepository.AddClub(club2);
            var response = clubRepository.GetClub(addedClub1.Id);

            Assert.NotEqual(response, addedClub2);
            var responseJson = JsonConvert.SerializeObject(response);
            var addedClub1Json = JsonConvert.SerializeObject(addedClub1);
            Assert.Equal(responseJson, addedClub1Json);
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
    }
}
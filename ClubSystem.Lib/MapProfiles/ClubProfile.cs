using System.Linq;
using AutoMapper;
using ClubSystem.Lib.Models.Dtos;
using ClubSystem.Lib.Models.Entities;
using ClubSystem.Lib.Models.Resources;

namespace ClubSystem.Lib.MapProfiles
{
    public class ClubProfile : Profile
    {
        public ClubProfile()
        {
            CreateMap<ClubDto, Club>()
                .ForMember(club => club.UserClubs, p => p.MapFrom(clubDto => clubDto.Members.Select(userDto => new UserClub{UserId = userDto.UserId})));
            CreateMap<Club, ClubResource>()
                .ForMember(club => club.Members, p => p.MapFrom(club => club.UserClubs.Select(userClub => new UserResource { Id = userClub.UserId })));
        }
    }
}
using System;
using System.Linq;
using AutoMapper;
using ClubSystem.Lib.Models.Dtos;
using ClubSystem.Lib.Models.Entities;
using ClubSystem.Lib.Models.Resources;

namespace ClubSystem.Lib.MapProfiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostDto, Post>()
//                .ForMember(post => post.Club, p => p.MapFrom(postDto => new Club { Id = postDto.ClubId }))
                .ForMember(post => post.UserPosts, p => p.MapFrom(postDto => postDto.UserIds.Select(userId => new UserPost { UserId = userId })))
                .ForMember(post => post.CreatedDate, p => p.MapFrom(postDto => DateTime.Now));

            CreateMap<Post, PostResource>()
//                .ForMember(postResource => postResource.ClubId, p => p.MapFrom(post => post.Club.Id))
                .ForMember(postResource => postResource.Users, p => p.MapFrom(post => post.UserPosts.Select(userPost => new UserResource { Id = userPost.UserId })));
        }
    }
}
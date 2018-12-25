using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ClubSystem.Lib.Exceptions;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models.Dtos;
using ClubSystem.Lib.Models.Entities;
using ClubSystem.Lib.Models.Resources;
using ClubSystem.Lib.Validators;
using Microsoft.EntityFrameworkCore;

namespace ClubSystem.Lib.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly ClubSystemDbContext _context;

        public PostRepository(ClubSystemDbContext context) : base(context)
        {
            _context = context;
        }

        public Post GetPost(string id)
        {
            return _context.Posts
                .Where(post => post.Id == id)
                .Include(post => post.UserPosts)
                .SingleOrDefault();
        }

        public ICollection<Post> GetAllPosts()
        {
            return _context.Set<Post>().Include(post => post.UserPosts).ToList();
        }

        public PostResource AddPost(PostDto postDto)
        {
            #region Checks
            if (postDto == null)
                throw new PostCannotBeNullException();

            var postValidator = new PostValidator();
            var validationResult = postValidator.Validate(postDto);

            if (!validationResult.IsValid)
                throw new PostIsNotValidException(validationResult.Errors.First().ErrorMessage);
            #endregion

            var newPost = new Post
            {
                Title = postDto.Title,
                Text = postDto.Text,
                MediaId = postDto.MediaId,
                UserPosts = new Collection<UserPost>(),
                Club = new Club { Id = postDto.ClubId },
                CreatedDate = DateTime.Now,
            };

            foreach (var userId in postDto.UserIds)
            {
                newPost.UserPosts.Add(new UserPost { UserId = userId });
            }

            //foreach (var clubId in postDto.ClubIds)
            //{
            //    newPost.ClubPosts.Add(new ClubPost { ClubId = clubId });
            //}

            _context.Posts.Add(newPost);
            _context.SaveChanges();

            var postResource = new PostResource
            {
                Id = newPost.Id,
                Text = newPost.Text,
                Title = newPost.Title,
                MediaId = newPost.MediaId,
                Users = new Collection<UserResource>(),
                ClubId = newPost.Club?.Id, // TODO: this could be enabled after Post entity refactor
                CreatedDate = newPost.CreatedDate,
                LastModifiedDate = newPost.LastModifiedDate
            };
            
            foreach (var userPost in newPost.UserPosts)
            {
                postResource.Users.Add(new UserResource { Id = userPost.UserId, Name = userPost.User?.UserName });
            }

            //var resourceClubId = newPost.ClubPosts?.ToList()[0].ClubId;
            //postResource.ClubId = resourceClubId;
            return postResource;
        }
    }
}
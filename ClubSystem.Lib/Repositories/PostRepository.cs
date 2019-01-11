using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClubSystem.Lib.Exceptions;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.MapProfiles;
using ClubSystem.Lib.Models.Dtos;
using ClubSystem.Lib.Models.Entities;
using ClubSystem.Lib.Models.Resources;
using ClubSystem.Lib.Validators;
using Microsoft.EntityFrameworkCore;

namespace ClubSystem.Lib.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly ClubSystemDbContext _context;
        private readonly IMapper _mapper;

        public PostRepository(ClubSystemDbContext context) : base(context)
        {
            _context = context;

            var config = new MapperConfiguration(cfg => { cfg.AddProfile<PostProfile>(); });
            _mapper = config.CreateMapper();
        }

        public PostResource GetPost(string id)
        {
            var post = _context.Set<Post>().SingleOrDefault(x => x.Id == id);
            var club = _context.Clubs.Find(post?.ClubId);

            var postResource = _mapper.Map<PostResource>(post);
            if (club != null)
            {
                postResource.ClubName = club.Name;
            }

            return postResource;
        }

        public ICollection<PostResource> GetAllPosts()
        {
            var posts = _context.Set<Post>().Include(post => post.UserPosts).ToList();

            var postResources = posts.Select(post => _mapper.Map<PostResource>(post)).ToList();
            var allClubs = _context.Clubs.ToList();

            if (allClubs.Count <= 0) return postResources;
            foreach (var postResource in postResources)
            {
                var foundedClub = allClubs.Find(club => club.Id == postResource.ClubId);
                if (foundedClub != null)
                {
                    postResource.ClubName = foundedClub.Name;
                }
            }

            return postResources;
        }

        /// <summary>
        /// This method gets PostResources of given clubIds
        /// </summary>
        /// <param name="clubIds">Collection of clubIds of requested posts.</param>
        /// <returns>A collection of PostResource</returns>
        /// <exception cref="Exception">When clubId has no post</exception>
        public async Task<ICollection<PostResource>> GetPostByClubIds(IEnumerable<string> clubIds)
        {
            var posts = new Collection<Post>();
            foreach (var clubId in clubIds)
            {
                var foundedPosts = await _context.Posts.Where(post => post.ClubId == clubId).ToListAsync();
                // if (foundedPosts.Count == 0) throw new Exception($"ClubId has no post, clubId: {clubId}");
                foundedPosts.ForEach(foundedPost => posts.Add(foundedPost));
            }

            return posts.Select(post => _mapper.Map<PostResource>(post)).ToList();
        }

        /// <summary>
        /// This method gets posts of the clubs which user subscribed to.
        /// </summary>
        /// <param name="userId">UserId of requested post feed</param>
        /// <returns>Returns a collection of PostResource</returns>
        /// <exception cref="ArgumentException">When given userId is not found in database</exception>
        /// <exception cref="Exception">When given userId's UserClubs is empty</exception>
        public async Task<ICollection<PostResource>> GetMyPostFeedAsync(string userId)
        {
            // var user = await _context.Users.FindAsync(userId);
            var foundedUser = await _context.Users
                .Include(user => user.UserClubs)
                .FirstOrDefaultAsync(user => user.Id == userId);

            if (foundedUser == null) throw new ArgumentException("Given userId is wrong");
            if (foundedUser.UserClubs == null) throw new NullReferenceException("Given user's UserClubs is null");
            if (foundedUser.UserClubs.Count == 0) throw new Exception("Given userId's UserClubs is empty");
            var clubIds = foundedUser.UserClubs.Select(userClub => userClub.ClubId).ToList();

            var postResources = await GetPostByClubIds(clubIds);

            return postResources;
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

            var newPost = _mapper.Map<Post>(postDto);

            _context.Posts.Add(newPost);
            _context.SaveChanges();

            var postResource = _mapper.Map<PostResource>(newPost);

            return postResource;
        }
    }
}
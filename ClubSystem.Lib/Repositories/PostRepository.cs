using System.Collections.Generic;
using System.Linq;
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

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PostProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public PostResource GetPost(string id)
        {
            var post = _context.Set<Post>().SingleOrDefault(x => x.Id == id);

            return _mapper.Map<PostResource>(post);
        }

        public ICollection<PostResource> GetAllPosts()
        {
            var posts = _context.Set<Post>().Include(post => post.UserPosts).ToList();

            return posts.Select(post => _mapper.Map<PostResource>(post)).ToList();
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
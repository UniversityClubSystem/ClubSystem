using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ClubSystem.Lib.Interfaces;
using ClubSystem.Lib.Models.Entities;
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

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Set<Post>().Include(post => post.UserPosts).ToList();
        }

        public Post GetPost(int id)
        {
            return _context.Posts.Where(post => post.Id == id).Include(post => post.UserPosts).SingleOrDefault();
        }

        public Post AddPost(Post post)
        {
            var newPost = new Post
            {
                Title = post.Title,
                Text = post.Text,
                CreatedDate = DateTime.Now,
                MediaId = post.MediaId,
                UserPosts = new Collection<UserPost>()
            };

            foreach (var userPost in post.UserPosts)
            {
                newPost.UserPosts.Add(userPost);
            }
            
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post;
        }
    }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClubSystem.Lib.Models.Entities
{
    public class Post : BaseModel
    {
        public string Title { get; set; } // TODO: change Title => Content
        public string Text { get; set; }
        public string MediaId { get; set; } // TODO: change int => string
        public ICollection<UserPost> UserPosts { get; set; }
        public ICollection<ClubPost> ClubPosts { get; set; } // TODO: change ICollection => string, a post can have only one club 

        public Post()
        {
            UserPosts = new Collection<UserPost>();
            ClubPosts = new Collection<ClubPost>();
        }
    }
}
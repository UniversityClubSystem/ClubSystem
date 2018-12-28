using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClubSystem.Lib.Models.Entities
{
    public class Post : BaseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string MediaId { get; set; }
        public Club Club { get; set; }
        public ICollection<UserPost> UserPosts { get; set; }

        public Post()
        {
            UserPosts = new Collection<UserPost>();
        }
    }
}
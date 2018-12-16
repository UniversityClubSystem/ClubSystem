using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClubSystem.Lib.Models.Entities
{
    public class Post : BaseModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int MediaId { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }

        public Post()
        {
            Users = new Collection<ApplicationUser>();
        }
    }
}
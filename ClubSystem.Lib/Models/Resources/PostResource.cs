using System.Collections.Generic;

namespace ClubSystem.Lib.Models.Resources
{
    public class PostResource : BaseResource
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string ClubId { get; set; }
        public string MediaId { get; set; }
        public ICollection<UserResource> Users { get; set; }

        public PostResource()
        {
            Users = new List<UserResource>();
        }
    }
}
using System.Collections.Generic;

namespace ClubSystem.Lib.Models.Resources
{
    public class UserResource : BaseResource
    {
        public string Name { get; set; }
        public ICollection<ClubResource> Clubs { get; set; }
        public ICollection<PostResource> Posts { get; set; }
    }
}

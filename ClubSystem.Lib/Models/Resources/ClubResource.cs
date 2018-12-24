using System.Collections.Generic;

namespace ClubSystem.Lib.Models.Resources
{
    public class ClubResource : BaseResource
    {
        public string Name { get; set; }
        public string UniversityName { get; set; }
        public ICollection<UserResource> Members { get; set; }
        public ICollection<PostResource> Posts { get; set; }
    }
}

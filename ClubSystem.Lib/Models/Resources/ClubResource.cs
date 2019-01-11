using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClubSystem.Lib.Models.Resources
{
    public class ClubResource : BaseResource
    {
        public string Name { get; set; }
        public string UniversityName { get; set; }
        public ICollection<string> Members { get; set; } = new Collection<string>();
        public ICollection<PostResource> Posts { get; set; } = new Collection<PostResource>();
    }
}
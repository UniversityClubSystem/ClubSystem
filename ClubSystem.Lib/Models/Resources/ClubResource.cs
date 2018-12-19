using System.Collections.Generic;

namespace ClubSystem.Lib
{
    class ClubResource : BaseResource
    {
        public string Name { get; set; }
        public ICollection<UserResource> Users { get; set; }
    }
}

using System.Collections.Generic;

namespace ClubSystem.Lib
{
    class UserResource : BaseResource
    {
        public string Name { get; set; }
        public ICollection<ClubResource> Clubs { get; set; }
    }
}

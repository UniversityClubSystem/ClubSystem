using System;
using System.Collections.Generic;
using System.Text;

namespace ClubSystem.Lib
{
    class UserResource : BaseResource
    {
        public string Name { get; set; }
        public ICollection<ClubResource> Clubs { get; set; }
    }
}

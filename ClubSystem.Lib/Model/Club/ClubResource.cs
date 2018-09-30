using System;
using System.Collections.Generic;
using System.Text;

namespace ClubSystem.Lib
{
    class ClubResource : BaseResource
    {
        public string Name { get; set; }
        public ICollection<UserResource> Users { get; set; }
    }
}

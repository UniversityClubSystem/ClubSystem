using ClubSystem.Lib.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ClubSystem.Lib.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<UserPost> UserPosts { get; set; }
        public virtual ICollection<UserClub> UserClubs { get; set; }
    }
}
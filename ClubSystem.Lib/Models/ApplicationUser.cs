using ClubSystem.Lib.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ClubSystem.Lib.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Club> Clubs { get; set; }
    }
}
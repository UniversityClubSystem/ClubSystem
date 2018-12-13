﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubSystem.Lib.Models.Entities
{
    [Table("Users")]
    public class User : BaseModel
    {
        [Required]
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public ICollection<UserClub> UserClubs { get; set; }

        public User()
        {
            UserClubs = new Collection<UserClub>();
        }
    }
}
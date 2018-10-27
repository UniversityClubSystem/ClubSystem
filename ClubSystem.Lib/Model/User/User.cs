using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClubSystem.Lib.Model.Base;

namespace ClubSystem.Lib.Model.User
{
    [Table("Users")]
    public class User : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public ICollection<UserClub> UserClubs { get; set; }

        public User()
        {
            UserClubs = new Collection<UserClub>();
        }
    }
}

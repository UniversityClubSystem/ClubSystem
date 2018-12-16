using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubSystem.Lib.Models.Entities
{
    [Table("Clubs")]
    public class Club : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string UniversityName { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }

        public Club()
        {
            Users = new Collection<ApplicationUser>();
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ClubSystem.Lib.Model.Base;

namespace ClubSystem.Lib.Model.Club
{
    [Table("Clubs")]
    public class ClubEntity : BaseModel
    {
        public string Name { get; set; }
        public string UniversityName { get; set; }
        public virtual ICollection<UserClubEntity> UserClubEntities { get; set; }
        
        // TODO: public ICollection<UserEntity> Admins { get; set; }
    }
}

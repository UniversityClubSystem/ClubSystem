using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ClubSystem.Lib.Model.Base;

namespace ClubSystem.Lib.Model.User
{
    [Table("Users")]
    public class UserEntity : BaseModel
    {
        public string Name { get; set; }
        public ICollection<UserClubEntity> UserClubEntities { get; set; }
    }
}

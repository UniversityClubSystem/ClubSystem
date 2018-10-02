using System.Collections.Generic;
using ClubSystem.Lib.Model.Base;

namespace ClubSystem.Lib.Model.User
{
    public class UserEntity : BaseModel
    {   
        public string Name { get; set; }
        public ICollection<UserClubEntity> UserClubEntities { get; set; }
    }
}

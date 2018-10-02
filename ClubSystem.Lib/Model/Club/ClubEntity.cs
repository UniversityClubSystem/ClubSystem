using System.Collections.Generic;
using ClubSystem.Lib.Model.Base;

namespace ClubSystem.Lib.Model.Club
{
    public class ClubEntity : BaseModel
    {
        public string Name { get; set; }
        public string UniversityName { get; set; }
        public ICollection<UserClubEntity> UserClubEntities { get; set; }
        
        // TODO: public ICollection<UserEntity> Admins { get; set; }
    }
}

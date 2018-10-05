using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ClubSystem.Lib.Model.Base;

namespace ClubSystem.Lib.Model.Club
{
    [Table("Clubs")]
    public class Club : BaseModel
    {
        public string Name { get; set; }
        public string UniversityName { get; set; }
    }
}

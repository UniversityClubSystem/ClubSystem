using System.Collections.Generic;

namespace ClubSystem.Lib.Models.Dtos
{
    public class ClubDto
    {
        public string Name { get; set; }
        public string UniversityName { get; set; }
        public ICollection<UserDto> Members { get; set; }
    }
}

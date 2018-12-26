using System.Collections.Generic;

namespace ClubSystem.Lib.Models.Dtos
{
    class UserDto
    {
        public string Name { get; set; }
        public ICollection<ClubDto> Clubs { get; set; }
    }
}

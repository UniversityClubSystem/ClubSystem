using System.Collections.Generic;

namespace ClubSystem.Lib
{
    class UserDto
    {
        public string Name { get; set; }
        public ICollection<ClubDto> Clubs { get; set; }
    }
}

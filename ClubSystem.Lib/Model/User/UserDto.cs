using System.Collections.Generic;

namespace ClubSystem.Lib
{
    class UserDto : BaseDto
    {
        public string Name { get; set; }
        public ICollection<ClubDto> Clubs { get; set; }
    }
}

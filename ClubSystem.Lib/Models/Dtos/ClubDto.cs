using System.Collections.Generic;

namespace ClubSystem.Lib
{
    class ClubDto : BaseDto
    {
        public string Name { get; set; }
        public ICollection<UserDto> Users { get; set; }
    }
}

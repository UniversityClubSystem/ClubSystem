using System;
using System.Collections.Generic;
using System.Text;

namespace ClubSystem.Lib
{
    class UserDto : BaseDto
    {
        public string Name { get; set; }
        public ICollection<ClubDto> Clubs { get; set; }
    }
}

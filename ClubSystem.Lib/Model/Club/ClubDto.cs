using System;
using System.Collections.Generic;
using System.Text;

namespace ClubSystem.Lib
{
    class ClubDto : BaseDto
    {
        public string Name { get; set; }
        public ICollection<UserDto> Users { get; set; }
    }
}

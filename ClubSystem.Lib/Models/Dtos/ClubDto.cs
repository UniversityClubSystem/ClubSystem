using System.Collections.Generic;

namespace ClubSystem.Lib.Models.Dtos
{
    class ClubDto
    {
        public string Name { get; set; }
        public ICollection<UserDto> Users { get; set; }
    }
}

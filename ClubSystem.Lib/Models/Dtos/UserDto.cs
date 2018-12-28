using System.Collections.Generic;

namespace ClubSystem.Lib.Models.Dtos
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public ICollection<ClubDto> Clubs { get; set; }
    }
}

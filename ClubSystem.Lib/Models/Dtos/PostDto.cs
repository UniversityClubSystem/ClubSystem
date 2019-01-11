using System.Collections.Generic;

namespace ClubSystem.Lib.Models.Dtos
{
    public class PostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string MediaId { get; set; }
        public string ClubId { get; set; }
        public ICollection<string> UserIds { get; set; }
    }
}

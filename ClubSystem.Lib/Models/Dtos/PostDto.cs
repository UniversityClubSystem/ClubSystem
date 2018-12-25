using System.Collections.Generic;

namespace ClubSystem.Lib.Models.Dtos
{
    public class PostDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string MediaId { get; set; }
        public ICollection<string> UserIds { get; set; }
        public string ClubId { get; set; }
    }
}

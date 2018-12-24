using System.ComponentModel.DataAnnotations.Schema;

namespace ClubSystem.Lib.Models.Entities
{
    [Table("ClubPosts")]
    public class ClubPost : BaseModel
    {
        public string ClubId { get; set; }
        public Club Club { get; set; }
        public string PostId { get; set; }
        public Post Post { get; set; }
    }
}
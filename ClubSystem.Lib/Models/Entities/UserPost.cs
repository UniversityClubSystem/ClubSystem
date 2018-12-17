namespace ClubSystem.Lib.Models.Entities
{
    public class UserPost
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}

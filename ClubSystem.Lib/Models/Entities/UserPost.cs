namespace ClubSystem.Lib.Models.Entities
{
    public class UserPost
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string PostId { get; set; }
        public Post Post { get; set; }
    }
}

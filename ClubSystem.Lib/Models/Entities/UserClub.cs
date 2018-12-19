namespace ClubSystem.Lib.Models.Entities
{
    public class UserClub
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string ClubId { get; set; }
        public Club Club { get; set; }
    }
}

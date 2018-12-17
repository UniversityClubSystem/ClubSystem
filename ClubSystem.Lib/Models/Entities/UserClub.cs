namespace ClubSystem.Lib.Models.Entities
{
    public class UserClub
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}

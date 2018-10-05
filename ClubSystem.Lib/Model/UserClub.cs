using System.ComponentModel.DataAnnotations.Schema;

namespace ClubSystem.Lib.Model
{
    [Table("UserClubs")]
    public class UserClub
    {
        public int UserId { get; set; }
        public User.User User { get; set; }
        public int ClubId { get; set; }
        public Club.Club Club { get; set; }
    }
}
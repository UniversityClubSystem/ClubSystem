using System.ComponentModel.DataAnnotations.Schema;
using ClubSystem.Lib.Model.Club;
using ClubSystem.Lib.Model.User;

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
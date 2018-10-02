using System.ComponentModel.DataAnnotations.Schema;
using ClubSystem.Lib.Model.Club;
using ClubSystem.Lib.Model.User;

namespace ClubSystem.Lib.Model
{
    [Table("UserClubs")]
    public class UserClubEntity
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public int ClubId { get; set; }
        public ClubEntity Club { get; set; }
    }
}
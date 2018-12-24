using System;

namespace ClubSystem.Lib.Models.Resources
{
    public class BaseResource
    {
        public string Id { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public User LastEditedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}

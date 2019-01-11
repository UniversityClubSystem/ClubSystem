using System;

namespace ClubSystem.Lib.Models.Entities
{
    public class BaseModel
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
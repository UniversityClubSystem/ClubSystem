using System;

namespace ClubSystem.Lib.Models.Entities
{
    public class BaseModel
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}

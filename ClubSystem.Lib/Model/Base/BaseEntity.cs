using System;
using System.Collections.Generic;
using System.Text;

namespace ClubSystem.Lib.Model.Base
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}

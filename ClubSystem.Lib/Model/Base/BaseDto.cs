using System;
using System.Collections.Generic;
using System.Text;

namespace ClubSystem.Lib
{
    class BaseDto
    {
        public int Identifier { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastEditedBy { get; set; }
        public DateTime LastEditedDate { get; set; }
    }
}

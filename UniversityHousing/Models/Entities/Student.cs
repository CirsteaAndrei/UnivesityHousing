using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityHousing.Models.Entities
{
    public enum FeeStatus
    {
        Budgeted,
        FeePaying,
        PartiallyExempted
    }

    public class Student : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNP { get; set; }
        public string Faculty { get; set; }
        public FeeStatus FeeStatus { get; set; }
        public int? RoomId { get; set; } 
        public Room? Room { get; set; }   
    }
}

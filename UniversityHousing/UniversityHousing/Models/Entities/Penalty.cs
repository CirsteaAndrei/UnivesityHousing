using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityHousing.Models.Entities
{
    public class Penalty : BaseEntity
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public decimal Amount { get; set; }
        public DateTime PenaltyDate { get; set; }
    }
}

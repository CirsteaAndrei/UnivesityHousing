using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityHousing.Models.Entities
{
    public class Payment : BaseEntity
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string MonthForWhichPaymentIsMade { get; set; }
    }
}

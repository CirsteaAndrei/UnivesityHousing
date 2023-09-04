using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityHousing.Models.Entities
{
    public class Dormitory : BaseEntity
    {
        public int DormitoryNumber { get; set; }
        public decimal FeeAmount { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}

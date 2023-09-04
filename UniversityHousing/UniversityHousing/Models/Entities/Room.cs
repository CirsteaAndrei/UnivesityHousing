using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityHousing.Models.Entities
{
    public class Room : BaseEntity
    {
        public int RoomNumber { get; set; }
        public int DormitoryId { get; set; }
        public virtual Dormitory Dormitory { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}

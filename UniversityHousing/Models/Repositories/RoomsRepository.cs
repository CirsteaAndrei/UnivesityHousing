using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHousing.Models.Entities;

namespace UniversityHousing.Models.Repositories
{
    public class RoomsRepository : RepositoryBase<Room>
    {
        private readonly AppDbContext dbContext;
        public RoomsRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        internal IEnumerable<Room> GetRoomsByDormitoryId(int dormitoryId)
        {
            var rooms = GetAll();
            var result = rooms.Where(r => r.DormitoryId == dormitoryId).ToList();
            foreach (Room room in result)
            {
                room.Students = dbContext.Students.Where(s => s.RoomId == room.Id).ToList();
            }
            return result;
        }
    }
}

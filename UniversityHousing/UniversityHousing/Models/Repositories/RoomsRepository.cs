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
    }
}

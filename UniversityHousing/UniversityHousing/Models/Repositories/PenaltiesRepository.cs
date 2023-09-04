using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHousing.Models.Entities;

namespace UniversityHousing.Models.Repositories
{
    public class PenaltiesRepository : RepositoryBase<Penalty>
    {
        private readonly AppDbContext dbContext;
        public PenaltiesRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}

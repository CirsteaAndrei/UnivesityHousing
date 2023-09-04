using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHousing.Models.Entities;

namespace UniversityHousing.Models.Repositories
{
    public class DormitoriesRepository : RepositoryBase<Dormitory>
    {
        private readonly AppDbContext dbContext;
        public DormitoriesRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}

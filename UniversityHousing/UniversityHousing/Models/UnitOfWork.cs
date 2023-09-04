using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHousing.Models.Repositories;

namespace UniversityHousing.Models
{
    public class UnitOfWork
    {
        public StudentsRepository Students { get; set; }
        public RoomsRepository Rooms { get; set; }
        public DormitoriesRepository Dormitories { get; set; }
        public PaymentsRepository Payments { get; set; }
        public PenaltiesRepository Penalties { get; set; }

        private readonly AppDbContext _dbContext;

        public UnitOfWork
        (
            AppDbContext dbContext,
            StudentsRepository studentsRepository,
            RoomsRepository roomsRepository,
            DormitoriesRepository dormitoriesRepository,
            PaymentsRepository paymentsRepository,
            PenaltiesRepository penaltiesRepository
        )
        {
            _dbContext = dbContext;
            Students = studentsRepository;
            Rooms = roomsRepository;
            Dormitories = dormitoriesRepository;
            Payments = paymentsRepository;
            Penalties = penaltiesRepository; 
        }

        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                var errorMessage = "Error when saving to the database: "
                    + $"{exception.Message}\n\n"
                    + $"{exception.InnerException}\n\n"
                    + $"{exception.StackTrace}\n\n";

                Console.WriteLine(errorMessage);
            }
        }
    }
}

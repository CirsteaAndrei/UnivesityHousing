using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHousing.Models.Entities;

namespace UniversityHousing.Models.Repositories
{
    public class PaymentsRepository : RepositoryBase<Payment>
    {
        private readonly AppDbContext dbContext;
        public PaymentsRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Payment> GetUnpaidPayments(int studentId)
        {
            // Get all months for which payments have been made for the student
            var paidMonths = _dbContext.Payments
                                       .Where(p => p.StudentId == studentId)
                                       .Select(p => p.MonthForWhichPaymentIsMade)
                                       .ToList();

            // List of all months (you can adjust this based on your requirements)
            var allMonths = new List<string>
    {
        "January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    };

            // Find months for which no payment has been made
            var unpaidMonths = allMonths.Except(paidMonths);

            // Create a list of unpaid payments (this is just a representation and doesn't fetch from the database)
            var unpaidPayments = unpaidMonths.Select(month => new Payment
            {
                StudentId = studentId,
                AmountPaid = 0, // or whatever default value you want to use
                PaymentDate = DateTime.MinValue, // or another default value
                MonthForWhichPaymentIsMade = month
            });

            return unpaidPayments;
        }
    }
}

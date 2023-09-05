using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.EntityFrameworkCore;
using UniversityHousing.Models.Entities;

namespace UniversityHousing.Models
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                    .UseSqlServer("Server=localhost;Database=UniHousing;User Id=user;Password=pass;Encrypt=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Dormitory> Dormitories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
    }
}

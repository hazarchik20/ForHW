using BLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class CarDataContext : DbContext
    {
        public CarDataContext(DbContextOptions<CarDataContext> options)
        : base(options)
        {
        }
        public CarDataContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CarDB;Integrated Security=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Car>()
                .Property(x => x.Price)
                .IsRequired();
            modelBuilder.Entity<Car>()
               .Property(x => x.HourePower)
               .IsRequired();
            modelBuilder.Entity<Car>()
                .Property(x => x.YearBirth)
                .IsRequired();
            modelBuilder.Entity<Car>()
               .Property(x => x.Name)
               .HasMaxLength(100)
               .IsRequired();
        }
    }
}

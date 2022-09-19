using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class HRContext : DbContext
    {
        public HRContext(DbContextOptions<HRContext> option) : base(option)
        {
        }
        public virtual DbSet<Employee> Employees { set; get; }
        public virtual DbSet<Attendence> Attendences { set; get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
              .HasData(
              new Employee
              {
                  Id = 1,
                  Name = "John Doe",
                  Address = "Cairo",
                  BirthDate = DateTime.Parse("8/1/1987"),
                  MobileNo = "01050460225",
                  CreatedAt = DateTime.Now,
                  Email = "JohnDoeManager@gmail.com"
              }
          );
        }
    }
}

using EmployeeData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeData
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions options)
        : base(options)
        {
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(new Department
            {
                Id = 1,
                Name = "developer",
                IsDeleted = false,
                Created = DateTime.Now
              
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}

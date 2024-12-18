using EMP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Database
{
    public class EMSDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<PerformanceReview> PerformanceReviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasRequired(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Department>()
                .HasRequired(e => e.Employees)
                .WithMany(d => d.Department)
                .HasForeignKey(e => e.ManagerId);

            modelBuilder.Entity<PerformanceReview>()
                .HasRequired(r => r.Employee)
                .WithMany(e => e.PerformanceReviews)
                .HasForeignKey(r => r.EmployeeId);


            base.OnModelCreating(modelBuilder);
        }
    }
}

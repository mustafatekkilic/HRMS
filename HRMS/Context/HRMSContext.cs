using HRMS.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Context
{
    public class HRMSContext : IdentityDbContext<Users, UserRoles, int>
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HRMS;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<int>>().HasNoKey();
            modelBuilder.Entity<IdentityUserRole<int>>().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<int>>().HasNoKey();

        }
    }
}

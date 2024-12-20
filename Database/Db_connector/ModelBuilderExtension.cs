using Microsoft.EntityFrameworkCore;
using resource_manager_db.Models;

namespace resource_manager_db.Db_connector
{
    public static class ModelBuilderExtension
    {
        public static void ConfigureCompany(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasKey(c => c.Id);

            modelBuilder.Entity<Company>().HasIndex(c => c.Name).IsUnique();

            modelBuilder
                .Entity<Company>()
                .HasMany(c => c.Employees)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyID)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public static void ConfigureEmployee(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);

            modelBuilder
                .Entity<Employee>()
                .HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.CompanyID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>().HasIndex(e => e.Email).IsUnique();
        }
    }
}

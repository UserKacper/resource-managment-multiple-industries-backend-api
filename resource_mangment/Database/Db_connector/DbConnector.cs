using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using resource_manager_db.Models;

namespace resource_manager_db.Db_connector
{
    public class DbConnector : IdentityDbContext<Employee, Role, string> 
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbConnector (DbContextOptions<DbConnector> options) : base(options)
        {
        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
  
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureCompany(); 
            modelBuilder.ConfigureEmployee();
            modelBuilder.ConfigureRoles();
        }
    }
}

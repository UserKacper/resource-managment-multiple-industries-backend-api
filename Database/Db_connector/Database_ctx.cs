using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using resource_manager_db.Models;

namespace resource_manager_db.Db_connector
{
    public class Database_ctx : IdentityDbContext<Employee, IdentityRole, string>
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }

        public Database_ctx(DbContextOptions<Database_ctx> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureCompany();
            modelBuilder.ConfigureEmployee();
        }
    }
}

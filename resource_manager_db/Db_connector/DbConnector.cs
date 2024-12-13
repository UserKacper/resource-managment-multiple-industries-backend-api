using Microsoft.EntityFrameworkCore;
using resource_manager_db.Models;
using System.Data.Entity;

namespace resource_manager_db.Db_connector
{
    public class DbConnector : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees{ get; set; }



    }
}

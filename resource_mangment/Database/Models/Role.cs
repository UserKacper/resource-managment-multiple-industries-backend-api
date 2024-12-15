using Microsoft.AspNetCore.Identity;

namespace resource_manager_db.Models
{
    public class Role : IdentityRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}

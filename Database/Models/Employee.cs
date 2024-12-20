using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace resource_manager_db.Models
{
    public class Employee : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public int BuildingNumber { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string CompanyID { get; set; }

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public Company Company { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using resource_manager_db.Models;

namespace resource_mangment.Logic.DTO_s
{
    public class CompanyDTO
    {
        [Required, Key]
        public string Id { get; set; }

        [Required, MaxLength(32), MinLength(8)]
        public string Name { get; set; }

        [Required]
        public Industry Industry { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public int StreetNumber { get; set; }

        [Required]
        public int PhoneNumber { get; set; }

        [Required]
        public string EmailAddress { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<IdentityRole> Roles { get; set; }
    }
}

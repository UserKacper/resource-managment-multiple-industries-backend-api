using resource_manager_db.Models;
using System.ComponentModel.DataAnnotations;

namespace resource_mangment.Logic.DTO_s
{
    public class EmployeeDTO
    {
        [Required]
        public string Id { get; set; }
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
        public int StreetNumber { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string CompanyID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public ICollection<Role> Roles { get; set; }
    }
}

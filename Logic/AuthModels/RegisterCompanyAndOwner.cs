using System.ComponentModel.DataAnnotations;
using resource_manager_db.Models;

namespace resource_mangment.Logic.AuthModels
{
    public class RegisterCompanyAndOwner
    {
        //company
        [Required]
        public string CompanyName { get; set; }

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
        public int BuildingNumber { get; set; }

        [Required]
        public string NIP { get; set; }

        //companyOwner(employee)
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}

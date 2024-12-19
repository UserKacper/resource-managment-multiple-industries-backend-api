using System.ComponentModel.DataAnnotations;

namespace resource_mangment.Logic.AuthModels
{
    public class RegisterEmployee
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
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime? CreationDate { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

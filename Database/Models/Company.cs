using System.ComponentModel.DataAnnotations;

namespace resource_manager_db.Models
{
    public enum Industry
    {
        Beauty,
    }

    public class Company
    {
        [Required, Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, MaxLength(32)]
        public string Name { get; set; }

        [Required]
        public DateTime EstablishmentDate { get; set; } = DateTime.UtcNow;

        [Required]
        public Industry Industry { get; set; }

        [Required]
        public string NIP { get; set; }

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
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}

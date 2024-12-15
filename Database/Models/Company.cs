using System.ComponentModel.DataAnnotations;

namespace resource_manager_db.Models
{

    public enum Industry
    {
        Beauty
    }
    public class Company 
    {
        [Required, Key]
       public Guid Id { get; set; }
        [Required, MaxLength(32),MinLength(8)]
        public string Name { get; set; }
        public DateTime EstablishmentDate { get; set; }
        [Required]
        public Industry Industry { get; set; }
        [Required]
        public string City {  get; set; }
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
        public ICollection <Role> Roles { get; set; }
    }
}

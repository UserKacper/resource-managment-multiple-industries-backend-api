using System.ComponentModel.DataAnnotations;

namespace resource_manager_db.Models
{
    public class Employee
    {
        [Required]
        Guid Id { get; set; }
        [Required]
        string Name { get; set; }
        [Required]
        string Surname { get; set; }
        [Required]
        Role role { get; set; }
        [Required]
        string City { get; set; }
        [Required]
        string Province { get; set; }
        [Required]
        string Country { get; set; }
        [Required]
        string Street { get; set; }
        [Required]
        int StreetNumber {  get; set; }
        [Required]
        int PhoneNumber {  get; set; }
        [Required]
        Guid CompanyID { get; set; }
        [Required]
        string Email {  get; set; }
        [Required]
        string Password { get; set; }
        [Required]
        DateTime CreationDate { get; set; }
    }
}

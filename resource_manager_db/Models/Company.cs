using Microsoft.EntityFrameworkCore;
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
        Guid Id { get; set; }
        [Required, MaxLength(32),MinLength(8)]
        string Name { get; set; }
        DateTime EstablishmentDate { get; set; }
        [Required]
        Industry Industry { get; set; }
        [Required]
        string City {  get; set; }
        [Required]
        string Street { get; set; }
        [Required]   
        string Province { get; set; }
        [Required]
        string Country { get; set; }
        [Required]
        int StreetNumber { get; set; }
        [Required]
        int PhoneNumber { get; set; }
        [Required]
        string EmailAddress { get; set; }
    }
}

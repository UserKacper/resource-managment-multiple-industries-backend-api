using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace resource_manager_db.Models
{
    public class Employee : IdentityUser<string>
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
        public int StreetNumber {  get; set; }
        [Required]
        public int PhoneNumber {  get; set; }
        [Required]
        public Guid CompanyID { get; set; }
        [Required]
        public string Email {  get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public ICollection<Role> Roles {get; set;}
        public DateTime CreationDate { get; set; }
        public Company Company { get; set; }
    }
}

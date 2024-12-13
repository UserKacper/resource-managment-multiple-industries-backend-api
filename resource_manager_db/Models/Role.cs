using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace resource_manager_db.Models
{
    public class Role
    {
        [Required]
        string Name { get; set; }
    }
}

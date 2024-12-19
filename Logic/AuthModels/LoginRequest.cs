using System.ComponentModel.DataAnnotations;

namespace resource_mangment.Logic.AuthModels
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email potrzebny do zalogowania")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło potrzebne do zalogowania")]
        public string Password { get; set; }
    }
}

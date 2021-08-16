using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.AccountModels
{
    public class RegisterViewModel
    {
        [DisplayName("E-mail")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Parolă")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirmă parola")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }
    }
}

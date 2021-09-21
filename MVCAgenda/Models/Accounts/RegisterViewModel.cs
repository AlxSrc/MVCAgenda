using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Models.Accounts
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
        [Compare("Password", ErrorMessage = "Parolele nu corespund.")]
        public string ConfirmPassword { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Models.Accounts
{
    public class LoginViewModel
    {
        [DisplayName("E-mail")]
        [Required]
        public string Email { get; set; }

        [DisplayName("Parola")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}

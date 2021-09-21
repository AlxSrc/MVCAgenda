using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Models.Accounts.ManageAccount
{
    public class ProfileViewModel
    {
        [StringLength(60, MinimumLength = 1)]
        [DisplayName("Nume*")]
        [Required]
        public string FirstName { get; set; }

        [StringLength(60, MinimumLength = 1)]
        [DisplayName("Prenume")]
        public string LastName { get; set; }

        [StringLength(60, MinimumLength = 1)]
        [DisplayName("Username")]
        public string Username { get; set; }

        [DisplayName("Prenume")]
        public string ProfilePicture { get; set; }
    }
}

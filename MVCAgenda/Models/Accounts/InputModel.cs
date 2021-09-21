using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Models.Accounts
{
    public class InputModel
    {
        [Required]
        [Display(Name = "Nume")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Prenume")]
        public string LastName { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MVCAgenda.Models.BaseModels;

namespace MVCAgenda.Models.Patients
{
    public class PatientViewModel : BaseModel
    {
        [DisplayName("Detalii pacient")] 
        public int SheetPatientId { get; set; }


        [StringLength(60, MinimumLength = 1)]
        [DisplayName("Nume")]
        [Required] 
        public string FirstName { get; set; }


        [StringLength(60, MinimumLength = 1)]
        [DisplayName("Prenume")]
        public string LastName { get; set; }


        [StringLength(20, MinimumLength = 1)]
        [DisplayName("Număr de telefon")]
        [Phone(ErrorMessage = "Numar de telefon invalit")]
        [Required] 
        public string PhoneNumber { get; set; }


        [StringLength(60, MinimumLength = 0)]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "Adresa de e-mail invalida")]
        public string Mail { get; set; }


        [DisplayName("Lista neagră")]
        public string BlacklistText { get; set; }

        [DisplayName("Lista neagră")]
        public bool Blacklist { get; set; }
    }
}

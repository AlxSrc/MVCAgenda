using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.ViewModels
{
    public class PatientViewModel : BaseEntityModel
    {
        [DisplayName("Detalii Pacient")] 
        public int SheetPatientId { get; set; }


        [StringLength(60, MinimumLength = 1)]
        [DisplayName("Nume")]
        [Required] 
        public string FirstName { get; set; }


        [StringLength(60, MinimumLength = 1)]
        [DisplayName("Prenume")]
        public string SecondName { get; set; }


        [StringLength(20, MinimumLength = 1)]
        [DisplayName("Numar de telefon")]
        [Required] 
        public string PhonNumber { get; set; }


        [StringLength(60, MinimumLength = 0)]
        [DisplayName("Mail")] 
        public string Mail { get; set; }


        [DisplayName("Lista neagra")]
        public string Blacklist { get; set; }
    }
}

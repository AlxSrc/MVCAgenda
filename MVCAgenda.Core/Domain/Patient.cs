using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAgenda.Core.Domain
{
    public class Patient : BaseEntityModel
    {
        [DisplayName("Detalii Pacient")] 
        public int SheetPatientId { get; set; }


        [ForeignKey("SheetPatientId")] 
        public virtual SheetPatient SheetPatient { get; set; }


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
        [Required] 
        public bool Blacklist { get; set; }
    }
}

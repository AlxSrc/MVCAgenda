using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAgenda.Core.Domain
{
    public class PatientDto
    {
        [Key] 
        public int PatientId { get; set; }


        [DisplayName("Detalii Pacient")] 
        public int SheetPatientId { get; set; }


        [ForeignKey("SheetPatientId")] 
        public virtual SheetPatientDto SheetPatient { get; set; }
        public virtual AppointmentDto Programare { get; set; }


        [StringLength(60, MinimumLength = 1)]
        [DisplayName("Nume")]
        [Required] 
        public string FirstName { get; set; }


        [StringLength(60, MinimumLength = 1)]
        [DisplayName("Prenume")]
        [Required] 
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
        public int Blacklist { get; set; }


        [DisplayName("Sters")]
        [Required] 
        public int Visible { get; set; } = 1;
    }
}

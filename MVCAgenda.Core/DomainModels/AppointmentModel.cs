using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.DomainModels
{
    public class AppointmentModel
    {
        [Key] 
        public int AppointmentId { get; set; }


        [DisplayName("Pacient")]
        public int PatientId { get; set; }


        //Details about Patient
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
        public int Blacklist { get; set; }


        //Details about Medic
        public int MedicId { get; set; }

        [DisplayName("Medic")]
        public string Medic { get; set; }


        //Details about Room
        public int RoomId { get; set; }

        [DisplayName("Camera")]
        public string Camera { get; set; }


        //Details about Appoitment
        [DisplayName("Data consultatie")]
        [DataType(DataType.Date)]
        [Required] 
        public string AppointmentDate { get; set; }


        [DisplayName("Ora consultatie")]
        [DataType(DataType.Time)]
        [Required] 
        public string AppointmentHour { get; set; }


        [DisplayName("Procedura")]
        [Required] 
        public string Procedure { get; set; }


        [DisplayName("Efectuata")]
        [Required] 
        public int Made { get; set; } = 1;

        
        [StringLength(30, MinimumLength = 1)]
        [DisplayName("Programare realizata de")]
        [Required] 
        public string ResponsibleForAppointment { get; set; }


        [DisplayName("Data creeare consultatie")]
        [DataType(DataType.DateTime)]
        [Required] 
        public string AppointmentCreationDate { get; set; }


        [DisplayName("Sters")]
        [Required] 
        public int Visible { get; set; }


        [DisplayName("Comentarii")]
        public string Comments { get; set; }
    }
}
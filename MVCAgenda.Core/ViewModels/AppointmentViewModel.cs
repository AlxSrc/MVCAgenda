using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.ViewModels
{
    public class AppointmentViewModel : BaseEntityModel
    {
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
        public string Blacklist { get; set; }


        //Details about Medic
        [DisplayName("Medic")]
        public int MedicId { get; set; }

        [DisplayName("Medic")]
        public string Medic { get; set; }


        //Details about Room

        [DisplayName("Room")]
        public int RoomId { get; set; }

        [DisplayName("Camera")]
        public string Room { get; set; }


        //Details about Appoitment
        [DisplayName("Data")]
        [DataType(DataType.Date)]
        [Required] 
        public string AppointmentDate { get; set; }


        [DisplayName("Ora")]
        [DataType(DataType.Time)]
        [Required] 
        public string AppointmentHour { get; set; }


        [DisplayName("Procedura")]
        [Required] 
        public string Procedure { get; set; }


        [DisplayName("Efectuata")]
        [Required] 
        public bool Made { get; set; }

        [DisplayName("Efectuata")]
        public string MadeText { get; set; }

        [StringLength(30, MinimumLength = 1)]
        [DisplayName("Programare realizata de")]
        [Required] 
        public string ResponsibleForAppointment { get; set; }


        [DisplayName("Data creeare consultatie")]
        [DataType(DataType.DateTime)]
        [Required] 
        public string AppointmentCreationDate { get; set; }


        [DisplayName("Comentarii")]
        public string Comments { get; set; }
    }
}
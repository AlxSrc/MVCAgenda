using MVCAgenda.Models.BaseModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Models.Appointments
{
    public class AppointmentDetailsViewModel : BaseModel
    {
        [DisplayName("Nume")]
        public string FirstName { get; set; }


        [DisplayName("Prenume")]
        public string SecondName { get; set; }


        [DisplayName("Număr de telefon")]
        public string PhonNumber { get; set; }


        [DisplayName("E-mail")]
        public string Mail { get; set; }


        [DisplayName("Lista neagră")]
        public string Blacklist { get; set; }


        [DisplayName("Medic")]
        public string Medic { get; set; }


        [DisplayName("Camera")]
        public string Room { get; set; }


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


        [DisplayName("Efectuată")]
        public string MadeText { get; set; }


        [DisplayName("Programare realizată de")]
        public string ResponsibleForAppointment { get; set; }


        [DisplayName("Data creeare programare")]
        [DataType(DataType.DateTime)]
        public string AppointmentCreationDate { get; set; }


        [DisplayName("Comentarii")]
        public string Comments { get; set; }
    }
}

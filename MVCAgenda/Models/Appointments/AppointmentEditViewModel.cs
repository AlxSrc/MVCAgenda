using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MVCAgenda.Models.BaseModels;

namespace MVCAgenda.Models.Appointments
{
    public class AppointmentEditViewModel : BaseModel
    {
        [DisplayName("Pacient")]
        public int PatientId { get; set; }

        [DisplayName("Medic")]
        public int MedicId { get; set; }

        [DisplayName("Room")]
        public int RoomId { get; set; }


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
        [Required]
        public bool Made { get; set; }


        [StringLength(30, MinimumLength = 1)]
        [DisplayName("Programare realizată de")]
        [Required]
        public string ResponsibleForAppointment { get; set; }


        [DisplayName("Data creeare programare")]
        [DataType(DataType.DateTime)]
        [Required]
        public string AppointmentCreationDate { get; set; }


        [DisplayName("Comentarii")]
        public string Comments { get; set; }
    }
}

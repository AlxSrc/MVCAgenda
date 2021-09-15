using MVCAgenda.Models.BaseModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Models.Appointments
{
    public class AppointmentCreateViewModel
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
        public string SecondName { get; set; }


        [StringLength(20, MinimumLength = 1)]
        [DisplayName("Număr de telefon")]
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string PhonNumber { get; set; }


        [StringLength(60, MinimumLength = 0)]
        [DisplayName("E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }


        //Details about Medic
        [DisplayName("Medic")]
        public int MedicId { get; set; }


        [DisplayName("Camera")]
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


        [StringLength(40, MinimumLength = 1)]
        [Required]
        public string ResponsibleForAppointment { get; set; }


        [DisplayName("Comentarii")]
        public string Comments { get; set; }
    }
}

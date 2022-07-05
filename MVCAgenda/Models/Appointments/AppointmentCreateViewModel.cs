using System;
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
        [DisplayName("Nume *")]
        [Required]
        public string FirstName { get; set; }


        [StringLength(60, MinimumLength = 1)]
        [DisplayName("Prenume")]
        public string LastName { get; set; }


        [StringLength(20, MinimumLength = 1)]
        [DisplayName("Număr de telefon *")]
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string PhoneNumber { get; set; }


        [StringLength(60, MinimumLength = 0)]
        [DisplayName("E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }


        //Details about Medic
        [DisplayName("Medic *")]
        public int MedicId { get; set; }


        [DisplayName("Camera")]
        public int RoomId { get; set; }


        [DisplayName("Dată început *")]
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;


        [DisplayName("Dată sfârșit")]
        public DateTime? EndDate { get; set; } = DateTime.Now;


        [DisplayName("Procedura *")]
        [Required]
        public string Procedure { get; set; }

        [DisplayName("Programare privată")]
        public bool PrivateAppointment { get; set; }


        [StringLength(40, MinimumLength = 1)]
        [Required]
        public string ResponsibleForAppointment { get; set; }


        [DisplayName("Comentarii")]
        public string Comments { get; set; }

        public DateTime CurrentDate { get; set; }
    }
}
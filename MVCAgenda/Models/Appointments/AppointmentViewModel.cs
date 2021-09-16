using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MVCAgenda.Models.BaseModels;

namespace MVCAgenda.Models.Appointments
{
    public class AppointmentViewModel : BaseModel
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
        [Required]
        public string PhonNumber { get; set; }


        [StringLength(60, MinimumLength = 0)]
        [DisplayName("E-mail")]
        public string Mail { get; set; }


        [DisplayName("Lista neagră")]
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


        [DisplayName("Dată început")]
        [Required]
        public DateTime StartDate { get; set; }


        [DisplayName("Dată sfârșit")]
        public DateTime EndDate { get; set; }


        [DisplayName("Procedura")]
        [Required] 
        public string Procedure { get; set; }


        [DisplayName("Efectuată")]
        [Required] 
        public bool Made { get; set; }

        [DisplayName("Efectuată")]
        public string MadeText { get; set; }

        [StringLength(30, MinimumLength = 1)]
        [DisplayName("Programare realizată de")]
        [Required] 
        public string ResponsibleForAppointment { get; set; }


        [DisplayName("Data creeare programare")]
        [Required] 
        public DateTime AppointmentCreationDate { get; set; }


        [DisplayName("Comentarii")]
        public string Comments { get; set; }
    }
}
using MVCAgenda.Models.BaseModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Models.Appointments
{
    public class AppointmentDetailsViewModel : BaseModel
    {
        public int PatientId { get; set; }

        [DisplayName("Nume")]
        public string FirstName { get; set; }


        [DisplayName("Prenume")]
        public string LastName { get; set; }


        [DisplayName("Număr de telefon")]
        public string PhoneNumber { get; set; }


        [DisplayName("E-mail")]
        public string Mail { get; set; }


        [DisplayName("Lista neagră")]
        public string Blacklist { get; set; }


        [DisplayName("Medic")]
        public string Medic { get; set; }


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
        public string MadeText { get; set; }


        [DisplayName("Programare realizată de")]
        public string ResponsibleForAppointment { get; set; }


        [DisplayName("Data creeare programare")]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentCreationDate { get; set; }


        [DisplayName("Comentarii")]
        public string Comments { get; set; }
    }
}
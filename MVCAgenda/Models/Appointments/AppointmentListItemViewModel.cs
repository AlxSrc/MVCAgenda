using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MVCAgenda.Models.BaseModels;
using System;

namespace MVCAgenda.Models.Appointments
{
    public class AppointmentListItemViewModel : BaseModel
    {
        [DisplayName("Pacient")]
        public int PatientId { get; set; }

        [DisplayName("Nume")]
        public string FirstName { get; set; }

        [DisplayName("Număr de telefon")]
        public string PhoneNumber { get; set; }

        [DisplayName("Email")]
        public string Mail { get; set; }


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
        public string Procedure { get; set; }
    }
}
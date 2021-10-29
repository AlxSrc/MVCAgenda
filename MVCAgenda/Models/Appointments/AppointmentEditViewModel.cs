using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MVCAgenda.Models.BaseModels;
using System;

namespace MVCAgenda.Models.Appointments
{
    public class AppointmentEditViewModel : BaseModel
    {
        [DisplayName("Pacient")]
        public int PatientId { get; set; }

        public string PatientName { get; set; }

        [DisplayName("Medic")]
        public int MedicId { get; set; }

        [DisplayName("Room")]
        public int RoomId { get; set; }


        [DisplayName("Dată început*")]
        //[DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;


        [DisplayName("Dată sfârșit")]
        public DateTime? EndDate { get; set; } = DateTime.Now;


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
        public DateTime AppointmentCreationDate { get; set; }


        [DisplayName("Comentarii")]
        public string Comments { get; set; }
    }
}
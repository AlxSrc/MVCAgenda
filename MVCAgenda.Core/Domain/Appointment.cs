using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAgenda.Core.Domain
{
    public class Appointment : BaseEntityModel
    {
        [DisplayName("Pacient")]
        public int PatientId { get; set; }


        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }


        [DisplayName("Camera")]
        public int RoomId { get; set; }


        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }


        [DisplayName("Medicul")]
        public int MedicId { get; set; }


        [ForeignKey("MedicId")]
        public virtual Medic Medic { get; set; }


        [DisplayName("Data consultatie")]
        [DataType(DataType.Date)]
        [Required]
        public string AppointmentDate { get; set; }


        [DisplayName("Ora consultatie")]
        [DataType(DataType.Time)]
        [Required]
        public string AppointmentHour { get; set; }


        [StringLength(75, MinimumLength = 1)]
        [DisplayName("Procedura")]
        [Required]
        public string Procedure { get; set; } = "neidentificat";


        [DisplayName("Efectuata")]
        [Required]
        public bool Made { get; set; } = true;


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
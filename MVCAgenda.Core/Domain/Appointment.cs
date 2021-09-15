using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAgenda.Core.Domain
{
    public class Appointment : BaseEntity
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public virtual Consultation Room { get; set; }

        public int MedicId { get; set; }
        [ForeignKey("MedicId")]
        public virtual Medic Medic { get; set; }


        [DataType(DataType.Date)]
        [Required]
        public string AppointmentDate { get; set; }


        [DataType(DataType.Time)]
        [Required]
        public string AppointmentHour { get; set; }


        [StringLength(75, MinimumLength = 1)]
        [Required]
        public string Procedure { get; set; }


        [Required]
        public bool Made { get; set; } = true;


        [StringLength(30, MinimumLength = 1)]
        [Required]
        public string ResponsibleForAppointment { get; set; }


        [DataType(DataType.DateTime)]
        [Required]
        public string AppointmentCreationDate { get; set; }


        public string Comments { get; set; }
    }
}
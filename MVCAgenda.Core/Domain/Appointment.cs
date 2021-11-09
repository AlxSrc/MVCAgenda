using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAgenda.Core.Domain
{
    public class Appointment : BaseSoftDeleteEntity
    {
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

        public int MedicId { get; set; }

        [ForeignKey("MedicId")]
        public virtual Medic Medic { get; set; }


        [Required]
        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }


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
        public DateTime AppointmentCreationDate { get; set; }


        public string Comments { get; set; }
    }
}
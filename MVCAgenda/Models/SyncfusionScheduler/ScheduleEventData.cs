using System;

namespace MVCAgenda.Models.SyncfusionScheduler
{
    public class ScheduleEventData
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int MedicId { get; set; }
        public int RoomId { get; set; }


        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PhonNumber { get; set; }
        public string Mail { get; set; }


        public string Medic { get; set; }
        public string Room { get; set; }
        public string SecondaryColor { get; set; }
        public string PrimaryColor { get; set; }


        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Subject { get; set; }
        public string Description { get; set; }

        public bool Made { get; set; }
        public string ResponsibleForAppointment { get; set; }
        public string AppointmentCreationDate { get; set; }
        public string User { get; set; }
    }
}

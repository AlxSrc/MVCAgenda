using MVCAgenda.Core.Domain;
using MVCAgenda.Models.SyncfusionScheduler;
using System;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Scheduler
{
    public interface ISchedulerFactory
    {
        Task<ScheduleEventData> PrepereScheduleItemListViewModel(Appointment appointment, Patient patient, Medic medic, Room room);

        Task<ScheduleList> GetAsync(
            DateTime? searchByAppointmentStartDate = null,
            DateTime? searchByAppointmentEndDate = null,
            string? mail = null);
    }
}
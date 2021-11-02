using MVCAgenda.Models.SyncfusionScheduler;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Scheduler
{
    public interface ISchedulerManager
    {
        Task<string> CreateAsync(ScheduleEventData scheduleData);

        Task<ScheduleList> GetAsync(
            DateTime? searchByAppointmentStartDate = null,
            DateTime? searchByAppointmentEndDate = null,
            string? mail = null);

        Task<string> UpdateAsync(ScheduleEventData scheduleData);

        Task<string> DeleteAsync(int id);

        Task<string> HideAsync(int id);
    }
}
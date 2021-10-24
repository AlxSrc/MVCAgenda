using MVCAgenda.Models.SyncfusionScheduler;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Scheduler
{
    public interface ISchedulerManager
    {
        Task<string> CreateAsync(ScheduleEventData ScheduleData);

        Task<ScheduleList> GetAsync(string Mail);

        Task<string> UpdateAsync(ScheduleEventData ScheduleData);

        Task<string> DeleteAsync(int id);

        Task<string> HideAsync(int id);
    }
}
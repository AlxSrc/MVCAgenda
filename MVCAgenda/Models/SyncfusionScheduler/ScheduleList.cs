using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Models.SyncfusionScheduler
{
    public class ScheduleList
    {
        public ScheduleList()
        {
            AppointmentsSchedule = new List<ScheduleEventData>();
        }

        #region Lists

        public List<ScheduleEventData> AppointmentsSchedule { get; set; }

        #endregion
    }
}
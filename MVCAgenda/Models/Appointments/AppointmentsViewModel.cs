using MVCAgenda.Models.SyncfusionScheduler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Models.Appointments
{
    public class AppointmentsViewModel
    {
        public AppointmentsViewModel()
        {
            AppointmentsList = new List<AppointmentListItemViewModel>();
        }

        #region Param
        public List<AppointmentListItemViewModel> AppointmentsList { get; set; }
        
        public bool Hidden { get; set; }
        public bool Blacklist { get; set; }
        #endregion

        #region Param for search
        public string SearchByName { get; set; }

        public string SearchByPhoneNumber { get; set; }

        public string SearchByEmail { get; set; }

        [DataType(DataType.Date)]
        public string SearchByAppointmentDate { get; set; }

        [DataType(DataType.Time)]
        public string SearchByAppointmentHour { get; set; }

        public int SearchByMedic { get; set; }

        public int SearchByRoom { get; set; }

        public string SearchByProcedure { get; set; }
        #endregion
    }
}

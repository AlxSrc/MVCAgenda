using MVCAgenda.Models.SyncfusionScheduler;
using System;
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
        public bool Daily { get; set; }
        #endregion

        #region Param for search
        public string SearchByName { get; set; }

        public string SearchByPhoneNumber { get; set; }

        public string SearchByEmail { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime SearchByAppointmentStartDate { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime SearchByAppointmentEndDate { get; set; } = DateTime.Now;

        public int SearchByMedic { get; set; }

        public int SearchByRoom { get; set; }

        public string SearchByProcedure { get; set; }
        #endregion
    }
}

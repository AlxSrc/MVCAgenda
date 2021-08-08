using MVCAgenda.Core.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.MVCAgendaManagement
{
    public class MVCAgendaViewsManager
    {
        public MVCAgendaViewsManager()
        {
            PatientsList = new List<PatientViewModel>();
            AppointmentsList = new List<AppointmentViewModel>();
        }

        #region Lists
        public List<PatientViewModel> PatientsList { get; set; }

        public List<AppointmentViewModel> AppointmentsList { get; set; }
        #endregion

        #region ViewControllers

        public bool Hidden { get; set; }

        #endregion

        #region Param for search
        public string SearchByName { get; set; }

        public string SearchByPhoneNumber { get; set; }

        public string SearchByEmail { get; set; }

        public int SearchByMedic { get; set; }

        public int SearchByRoom { get; set; }

        [DataType(DataType.Date)]
        public string SearchByAppointmentDate { get; set; }

        [DataType(DataType.Time)]
        public string SearchByAppointmentHour { get; set; }
        #endregion
    }
}

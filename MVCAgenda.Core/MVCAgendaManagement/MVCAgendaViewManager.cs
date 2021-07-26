using MVCAgenda.Core.DomainModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Models.MVCAgendaManagement
{
    public class MVCAgendaViewManager
    {
        public MVCAgendaViewManager()
        {
            PatientsList = new List<PatientModel>();
            AppointmentsList = new List<AppointmentModel>();
        }

        #region Lists
        public List<PatientModel> PatientsList { get; set; }

        public List<AppointmentModel> AppointmentsList { get; set; }
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

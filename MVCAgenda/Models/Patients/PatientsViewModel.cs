using System.Collections.Generic;

namespace MVCAgenda.Models.Patients
{
    public class PatientsViewModel
    {
        public PatientsViewModel()
        {
            PatientsList = new List<PatientListItemViewModel>();
        }

        #region Lists
        public List<PatientListItemViewModel> PatientsList { get; set; }
        #endregion

        #region ViewControllers
        public bool Hidden { get; set; }
        public bool Blacklist { get; set; }
        #endregion

        #region Param for search
        public string SearchByName { get; set; }

        public string SearchByPhoneNumber { get; set; }

        public string SearchByEmail { get; set; }
        #endregion
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public bool? Hidden { get; set; }
        public bool? Blacklist { get; set; }

        #endregion

        #region Param for search

        [Display(Name = "Numele de familie")]
        public string SearchByName { get; set; }

        [Display(Name = "Numar de telefon")]
        public string SearchByPhoneNumber { get; set; }

        [Display(Name = "Adresa de e-mail")]
        public string SearchByEmail { get; set; }

        #endregion
    }
}
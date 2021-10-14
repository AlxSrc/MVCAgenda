using System.Collections.Generic;

namespace MVCAgenda.Models.Medics
{
    public class MedicsViewModel
    {
        public MedicsViewModel()
        {
            MedicsList = new List<MedicViewModel>();
        }

        #region Lists

        public List<MedicViewModel> MedicsList { get; set; }

        #endregion
    }
}

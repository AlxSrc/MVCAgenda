using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MVCAgenda.Models.BaseModels;

namespace MVCAgenda.Models.Consultations
{
    public class ConsultationsListItemViewModel : BaseModel
    {
        [DisplayName("Simptome")]
        public string Symptoms { get; set; }

        [DisplayName("Diagnostic")]
        public string Diagnostic { get; set; }

        [DisplayName("Prescripții")]
        public string Prescriptions { get; set; }
    }
}
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MVCAgenda.Models.BaseModels;

namespace MVCAgenda.Models.Consultations
{
    public class ConsultationEditViewModel : BaseModel
    {
        public int PatientSheetId { get; set; }

        [DisplayName("Nume")]
        public string FirstName { get; set; }

        [DisplayName("Prenume")]
        public string LastName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }


        [DisplayName("Simptome")]
        public string Symptoms { get; set; }


        [DisplayName("Diagnostic")]
        public string Diagnostic { get; set; }


        [DisplayName("Prescripții")]
        public string Prescriptions { get; set; }
    }
}
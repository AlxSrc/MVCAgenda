using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.ViewModels
{
    public class ConsultationViewModel : BaseModel
    {
        [DisplayName("Fisa Pacient")] 
        public int SheetPatientId { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayName("Data creeare")] 
        public DateTime CreationDate { get; set; }


        [StringLength(450, MinimumLength = 1)]
        [DisplayName("Simptome")]
        public string Symptom { get; set; }


        [StringLength(450, MinimumLength = 1)]
        [DisplayName("Diagnostic")]
        public string Diagnostic { get; set; }


        [StringLength(450, MinimumLength = 1)]
        [DisplayName("Prescriptii")]
        public string Prescriptions { get; set; }
    }
}

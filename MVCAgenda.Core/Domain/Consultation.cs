using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAgenda.Core.Domain
{
    public class Consultation : BaseSoftDeleteEntity
    {
        public int PatientSheetId { get; set; }

        [ForeignKey("SheetPatientId")]
        public virtual PatientSheet SheetPatient { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }


        public string Symptoms { get; set; }


        public string Diagnostic { get; set; }


        public string Prescriptions { get; set; }
    }
}
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAgenda.Core.Domain
{
    public class Consultation : BaseModel
    {
        [DisplayName("Fisa Pacient")] 
        public int SheetPatientId { get; set; }

        [ForeignKey("SheetPatientId")] 
        public virtual SheetPatient SheetPatient { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayName("Data creeare")] 
        public DateTime CreationDate { get; set; }


        [StringLength(450, MinimumLength = 1)]
        [DisplayName("Simptome")]
        public string Symptoms { get; set; }


        [StringLength(450, MinimumLength = 1)]
        [DisplayName("Diagnostic")]
        public string Diagnostic { get; set; }


        [StringLength(450, MinimumLength = 1)]
        [DisplayName("Prescriptii")]
        public string Prescriptions { get; set; }

        //[DisplayName("Sters")]
        //public int Visible { get; set; }

    }
}

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAgenda.Core.Domain
{
    public class PatientSheet : BaseSoftDeleteEntity
    {
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }


        public string AntecedentsH { get; set; }


        public string AntecedentsP { get; set; }


        public string NationalIdentificationNumber { get; set; }


        public int Gender { get; set; }


        [StringLength(60, MinimumLength = 1)]
        public string Town { get; set; }


        public string Street { get; set; }


        public string PhysicalExamination { get; set; }


        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
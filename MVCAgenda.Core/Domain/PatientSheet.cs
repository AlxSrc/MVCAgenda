using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAgenda.Core.Domain
{
    public class PatientSheet : BaseEntityDomain
    {
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }


        [StringLength(450, MinimumLength = 1)]
        public string AntecedentsH { get; set; }


        [StringLength(450, MinimumLength = 1)]
        public string AntecedentsP { get; set; }


        [StringLength(20, MinimumLength = 1)]
        public string NationalIdentificationNumber { get; set; }


        public int Gender { get; set; }


        [StringLength(60, MinimumLength = 1)]
        public string Town { get; set; }


        [StringLength(60, MinimumLength = 1)]
        public string Street { get; set; }


        [StringLength(60, MinimumLength = 1)]
        public string PhysicalExamination { get; set; }


        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
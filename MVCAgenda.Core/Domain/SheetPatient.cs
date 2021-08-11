using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.Domain
{
    public class SheetPatient : BaseEntityModel
    {
        public virtual Patient Patient { get; set; }


        [StringLength(450, MinimumLength = 1)]
        [DisplayName("Antecedente: heredo-colaterale")] 
        public string AntecedentsH { get; set; }


        [StringLength(450, MinimumLength = 1)]
        [DisplayName("Antecedente: Personale")] 
        public string AntecedentsP { get; set; }


        [StringLength(20, MinimumLength = 1)] 
        public string CNP { get; set; }


        [DisplayName("Sexul")]
        public int Gender { get; set; }


        [StringLength(60, MinimumLength = 1)]
        [DisplayName("Localitatea")]
        public string Town { get; set; }


        [StringLength(60, MinimumLength = 1)]
        [DisplayName("Strada")]
        public string Street { get; set; }


        [StringLength(60, MinimumLength = 1)]
        [DisplayName("Examen fizic")] 
        public string PhysicalExamination { get; set; }


        [DataType(DataType.Date)]
        [DisplayName("Data nasterii")] 
        public DateTime DateOfBirth { get; set; }
    }
}

using MVCAgenda.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.ViewModels
{
    public class SheetPatientViewModel : BaseModel
    {
        [DisplayName("Antecedente: heredo-colaterale")] 
        public string AntecedentsH { get; set; }


        [DisplayName("Antecedente: Personale")] 
        public string AntecedentsP { get; set; }


        [DisplayName("Examen fizic")]
        public string PhysicalExamination { get; set; }


        public string CNP { get; set; }


        [DisplayName("Sexul")]
        public string Gender { get; set; }


        [DisplayName("Localitatea")]
        public string Town { get; set; }


        [DisplayName("Strada")]
        public string Street { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayName("Data nasterii")] 
        public DateTime DateOfBirth { get; set; }

        public List<Consultation> Consultations { get; set; }
    }
}

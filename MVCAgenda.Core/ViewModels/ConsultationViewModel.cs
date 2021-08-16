﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.ViewModels
{
    public class ConsultationViewModel : BaseEntityModel
    {
        [DisplayName("Fișă pacient")] 
        public int SheetPatientId { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayName("Dată creeare")] 
        public DateTime CreationDate { get; set; }


        [StringLength(450, MinimumLength = 1)]
        [DisplayName("Simptome")]
        public string Symptoms { get; set; }


        [StringLength(450, MinimumLength = 1)]
        [DisplayName("Diagnostic")]
        public string Diagnostic { get; set; }


        [StringLength(450, MinimumLength = 1)]
        [DisplayName("Prescripții")]
        public string Prescriptions { get; set; }
    }
}

﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MVCAgenda.Models.BaseModels;

namespace MVCAgenda.Models.Consultations
{
    public class ConsultationEditViewModel : BaseModel
    {
        public int SheetPatientId { get; set; }

        [DataType(DataType.DateTime)]
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
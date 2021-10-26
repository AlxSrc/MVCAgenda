﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MVCAgenda.Models.BaseModels;

namespace MVCAgenda.Models.Consultations
{
    public class ConsultationCreateViewModel
    {
        public int PatientSheetId { get; set; }

        [DisplayName("Nume")]
        public string FirstName { get; set; }

        [DisplayName("Prenume")]
        public string LastName { get; set; }

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
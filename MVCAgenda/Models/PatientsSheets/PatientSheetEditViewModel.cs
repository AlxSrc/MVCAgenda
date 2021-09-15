﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MVCAgenda.Models.BaseModels;
using MVCAgenda.Models.Consultations;

namespace MVCAgenda.Models.PatientsSheets
{
    public class PatientSheetEditViewModel : BaseModel
    {
        [DisplayName("Antecedente: Heredo-colaterale")]
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

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}

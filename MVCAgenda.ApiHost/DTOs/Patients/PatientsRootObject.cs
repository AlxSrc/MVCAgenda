using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MVCAgenda.ApiHost.DTOs.Patients
{
    public class PatientsRootObject : ISerializableObject
    {
        public PatientsRootObject()
        {
            Patients = new List<PatientDto>();
        }

        [JsonProperty("patients")]
        public IList<PatientDto> Patients { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "patients";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(PatientDto);
        }
    }
}
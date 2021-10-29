using MVCAgenda.ApiHost.Models;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace MVCAgenda.ApiHost.DTOs.PatientSheets
{
    public class PatientSheetDto : BaseEntityDto
    {
        [JsonProperty("patient_id")]
        [JsonPropertyName("patient_id")]
        public int PatientId { get; set; }

        [JsonProperty("antecedents_h")]
        [JsonPropertyName("antecedents_h")]
        public string AntecedentsH { get; set; }

        [JsonProperty("antecedents_p")]
        [JsonPropertyName("antecedents_p")]
        public string AntecedentsP { get; set; }

        [JsonProperty("national_identification_number")]
        [JsonPropertyName("national_identification_number")]
        public string NationalIdentificationNumber { get; set; }

        [JsonProperty("gender")]
        [JsonPropertyName("gender")]
        public int Gender { get; set; }

        [JsonProperty("town")]
        [JsonPropertyName("town")]
        public string Town { get; set; }

        [JsonProperty("street")]
        [JsonPropertyName("street")]
        public string Street { get; set; }

        [JsonProperty("physical_examination")]
        [JsonPropertyName("physical_examination")]
        public string PhysicalExamination { get; set; }

        [JsonProperty("date_of_birth")]
        [JsonPropertyName("date_of_birth")]
        public DateTime DateOfBirth { get; set; }
    }
}

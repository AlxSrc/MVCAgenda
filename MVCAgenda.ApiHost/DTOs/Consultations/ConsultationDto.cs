using MVCAgenda.ApiHost.Models;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace MVCAgenda.ApiHost.DTOs.Consultations
{
    public class ConsultationDto : BaseEntityDto
    {
        [JsonProperty("patient_sheet_id")]
        [JsonPropertyName("patient_sheet_id")]
        public int PatientSheetId { get; set; }

        [JsonProperty("creation_date")]
        [JsonPropertyName("creation_date")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("symptoms")]
        [JsonPropertyName("symptoms")]
        public string Symptoms { get; set; }

        [JsonProperty("diagnostic")]
        [JsonPropertyName("diagnostic")]
        public string Diagnostic { get; set; }

        [JsonProperty("prescriptions")]
        [JsonPropertyName("prescriptions")]
        public string Prescriptions { get; set; }
    }
}

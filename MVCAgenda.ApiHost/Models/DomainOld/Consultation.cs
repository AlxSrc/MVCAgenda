using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MVCAgenda.ApiHost.Models.Domain
{
    public class Consultation : BaseEntityDto
    {
        [JsonProperty("sheet_patient_id")]
        [JsonPropertyName("sheet_patient_id")]
        public int SheetPatientId { get; set; }

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

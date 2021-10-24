using MVCAgenda.ApiHost.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MVCAgenda.ApiHost.DTOs.Patients
{
    public class PatientDto : BaseEntity
    {
        [JsonProperty("patient_sheet_id")]
        [JsonPropertyName("patient_sheet_id")]
        public int PatientSheetId { get; set; }

        [JsonProperty("first_name")]
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonProperty("phone_number")]
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("mail")]
        [JsonPropertyName("mail")]
        public string Mail { get; set; }

        [JsonProperty("status_code")]
        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }
    }
}
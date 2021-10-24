using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MVCAgenda.ApiHost.Models.Parameters.Patients
{
    public class BasePatientsParametersModel
    {
        public BasePatientsParametersModel()
        {
            IsDeleted = false;
            IncludeBlackList = false;
            SearchByName = string.Empty;
            SearchByPhoneNumber = string.Empty;
            SearchByEmail = string.Empty;
        }

        [JsonProperty("is_deleted")]
        [JsonPropertyName("is_deleted")]
        public bool? IsDeleted { get; set; }

        [JsonProperty("include_blackList")]
        [JsonPropertyName("include_blackList")]
        public bool? IncludeBlackList { get; set; }

        [JsonProperty("search_by_name")]
        [JsonPropertyName("search_by_name")]
        public string SearchByName { get; set; }

        [JsonProperty("search_by_phone_number")]
        [JsonPropertyName("search_by_phone_number")]
        public string SearchByPhoneNumber { get; set; }

        [JsonProperty("search_by_email")]
        [JsonPropertyName("search_by_email")]
        public string SearchByEmail { get; set; }
    }
}
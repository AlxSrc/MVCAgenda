using Newtonsoft.Json;

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
        public bool? IsDeleted { get; set; }

        [JsonProperty("include_blacklist")]
        public bool? IncludeBlackList { get; set; }

        [JsonProperty("name")]
        public string SearchByName { get; set; }

        [JsonProperty("phone-number")]
        public string SearchByPhoneNumber { get; set; }

        [JsonProperty("email")]
        public string SearchByEmail { get; set; }
    }
}

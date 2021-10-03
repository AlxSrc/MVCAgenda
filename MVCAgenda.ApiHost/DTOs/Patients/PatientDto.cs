using MVCAgenda.ApiHost.Models;
using Newtonsoft.Json;

namespace MVCAgenda.ApiHost.DTOs.Patients
{
    public class PatientDto : BaseEntity
    {
        [JsonProperty("patient_sheet_id")]
        public int PatientSheetId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("mail")]
        public string Mail { get; set; }

        [JsonProperty("blacklist")]
        public bool Blacklist { get; set; }
    }
}

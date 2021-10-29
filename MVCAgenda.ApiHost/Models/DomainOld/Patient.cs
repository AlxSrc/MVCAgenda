using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MVCAgenda.ApiHost.Models.Domain
{
    public class Patient : BaseEntityDto
    {

        [JsonProperty("first_name")]
        [JsonPropertyName("first_name")]
        public int SheetPatientId { get; set; }

        [JsonProperty("first_name")]
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("second_name")]
        [JsonPropertyName("second_name")]
        public string SecondName { get; set; }

        [JsonProperty("phone_number")]
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("mail")]
        [JsonPropertyName("mail")]
        public string Mail { get; set; }


        [JsonProperty("blacklist")]
        [JsonPropertyName("blacklist")]
        public bool Blacklist { get; set; }
    }
}

using MVCAgenda.ApiHost.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MVCAgenda.ApiHost.DTOs.Medics
{
    public class MedicDto : BaseEntityDto
    {
        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonProperty("image_path")]
        [JsonPropertyName("image_path")]
        public string ImagePath { get; set; }

        [JsonProperty("mail")]
        [JsonPropertyName("mail")]
        public string Mail { get; set; }

        [JsonProperty("Description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonProperty("designation")]
        [JsonPropertyName("designation")]
        public string Designation { get; set; }
    }
}

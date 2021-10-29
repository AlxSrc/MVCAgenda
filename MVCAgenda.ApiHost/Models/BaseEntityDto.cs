using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MVCAgenda.ApiHost.Models
{
    public class BaseEntityDto
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonProperty("hidden")]
        [JsonPropertyName("hidden")]
        public bool Hidden { get; set; }
    }
}

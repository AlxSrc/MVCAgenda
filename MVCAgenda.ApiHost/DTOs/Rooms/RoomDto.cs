using MVCAgenda.ApiHost.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MVCAgenda.ApiHost.DTOs.Rooms
{
    public class RoomDto : BaseEntityDto
    {
        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonProperty("primary_color")]
        [JsonPropertyName("primary_color")]
        public string PrimaryColor { get; set; }

        [JsonProperty("secondary_color")]
        [JsonPropertyName("secondary_color")]
        public string SecondaryColor { get; set; }

        [JsonProperty("description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}

using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MVCAgenda.ApiHost.Models.Domain
{
    public class Room : BaseEntityDto
    {
        [JsonProperty("medic_name")]
        [JsonPropertyName("medic_name")]
        public string RoomName { get; set; }
    }
}

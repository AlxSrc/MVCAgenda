using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MVCAgenda.ApiHost.Models.Domain
{
    public class Medic : BaseEntityDto
    {
        [JsonProperty("medic_name")]
        [JsonPropertyName("medic_name")]
        public string MedicName { get; set; }
    }
}

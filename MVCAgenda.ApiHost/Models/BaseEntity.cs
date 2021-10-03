using Newtonsoft.Json;

namespace MVCAgenda.ApiHost.Models
{
    public class BaseEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}

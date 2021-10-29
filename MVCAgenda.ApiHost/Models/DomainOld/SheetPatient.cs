using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MVCAgenda.ApiHost.Models.Domain
{
    public class SheetPatient : BaseEntityDto
    {
        [JsonProperty("antecedents_h")]
        [JsonPropertyName("antecedents_h")]
        public string AntecedentsH { get; set; }

        [JsonProperty("antecedents_p")]
        [JsonPropertyName("antecedents_p")]
        public string AntecedentsP { get; set; }

        [JsonProperty("cnp")]
        [JsonPropertyName("cnp")]
        public string CNP { get; set; }

        [JsonProperty("gender")]
        [JsonPropertyName("gender")]
        public int Gender { get; set; }

        [JsonProperty("town")]
        [JsonPropertyName("town")]
        public string Town { get; set; }

        [JsonProperty("street")]
        [JsonPropertyName("street")]
        public string Street { get; set; }

        [JsonProperty("physical_examination")]
        [JsonPropertyName("physical_examination")]
        public string PhysicalExamination { get; set; }

        [JsonProperty("date_of_birth")]
        [JsonPropertyName("date_of_birth")]
        public DateTime DateOfBirth { get; set; }
    }
}

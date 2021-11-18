using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MVCAgenda.ApiHost.DTOs.Consultations
{
    public class ConsultationRootObject : ISerializableObject
    {
        public ConsultationRootObject()
        {
            Consultations = new List<ConsultationDto>();
        }

        [JsonProperty("consultations")]
        public IList<ConsultationDto> Consultations { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "consultations";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(ConsultationDto);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MVCAgenda.ApiHost.DTOs.Medics
{
    public class MedicsRootObject : ISerializableObject
    {
        public MedicsRootObject()
        {
            Medics = new List<MedicDto>();
        }

        [JsonProperty("medics")]
        public IList<MedicDto> Medics { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "medics";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(MedicDto);
        }
    }
}

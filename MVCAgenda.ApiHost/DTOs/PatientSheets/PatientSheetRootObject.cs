using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace MVCAgenda.ApiHost.DTOs.PatientSheets
{
    public class PatientSheetRootObject : ISerializableObject
    {
        public PatientSheetRootObject()
        {
            PatientSheet = new PatientSheetDto();
        }

        [JsonProperty("patient_sheet")]
        [JsonPropertyName("patient_sheet")]
        public PatientSheetDto PatientSheet { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "patient_sheet";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(PatientSheetDto);
        }
    }
}

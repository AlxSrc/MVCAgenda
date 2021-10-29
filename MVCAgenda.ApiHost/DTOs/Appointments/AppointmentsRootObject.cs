using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MVCAgenda.ApiHost.DTOs.Appointments
{
    public class AppointmentsRootObject : ISerializableObject
    {
        public AppointmentsRootObject()
        {
            Appointments = new List<AppointmentCompleteDataDto>();
        }

        [JsonProperty("appointments")]
        public IList<AppointmentCompleteDataDto> Appointments { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "appointments";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(AppointmentCompleteDataDto);
        }
    }
}

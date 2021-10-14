using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MVCAgenda.ApiHost.DTOs.Appointments
{
    public class AppointmentsRootObject : ISerializableObject
    {
        public AppointmentsRootObject()
        {
            Appointments = new List<AppointmentDto>();
        }

        [JsonProperty("appointments")]
        public IList<AppointmentDto> Appointments { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "appointments";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(AppointmentDto);
        }
    }
}

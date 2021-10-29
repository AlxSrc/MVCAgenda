using MVCAgenda.ApiHost.DTOs.Appointments;
using MVCAgenda.ApiHost.DTOs.Consultations;
using MVCAgenda.ApiHost.DTOs.Patients;
using MVCAgenda.ApiHost.DTOs.PatientSheets;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MVCAgenda.ApiHost.DTOs.Items
{
    public class ItemsRootObject
    {
        [JsonProperty("patients")]
        public List<PatientDto> Patients { get; set; }
        [JsonProperty("patients_sheets")]
        public List<PatientSheetDto> PatientSheets { get; set; }
        [JsonProperty("consultations")]
        public List<ConsultationDto> Consultations { get; set; }
        [JsonProperty("appointments")]
        public List<AppointmentDto> Appointments { get; set; }

        public ItemsRootObject()
        {
            Patients = new List<PatientDto>();
            PatientSheets = new List<PatientSheetDto>();
            Consultations = new List<ConsultationDto>();
            Appointments = new List<AppointmentDto>();
        }
    }
}

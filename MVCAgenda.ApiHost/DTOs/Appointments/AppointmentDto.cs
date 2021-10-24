using MVCAgenda.ApiHost.Models;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace MVCAgenda.ApiHost.DTOs.Appointments
{
    public class AppointmentDto : BaseEntity
    {
        [JsonProperty("patient_id")]
        [JsonPropertyName("patient_id")]
        public int PatientId { get; set; }

        [JsonProperty("first_name")]
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonProperty("phone_number")]
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("mail")]
        [JsonPropertyName("mail")]
        public string Mail { get; set; }

        [JsonProperty("room_id")]
        [JsonPropertyName("room_id")]
        public int RoomId { get; set; }

        [JsonProperty("room_name")]
        [JsonPropertyName("room_name")]
        public string RoomName { get; set; }

        [JsonProperty("medic_id")]
        [JsonPropertyName("medic_id")]
        public int MedicId { get; set; }

        [JsonProperty("medic_name")]
        [JsonPropertyName("medic_name")]
        public string MedicName { get; set; }

        [JsonProperty("start_date")]
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("end_date")]
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }

        [JsonProperty("procedure")]
        [JsonPropertyName("procedure")]
        public string Procedure { get; set; }

        [JsonProperty("made")]
        [JsonPropertyName("made")]
        public bool Made { get; set; } = true;

        [JsonProperty("responsible_for_appointment")]
        [JsonPropertyName("responsible_for_appointment")]
        public string ResponsibleForAppointment { get; set; }

        [JsonProperty("appointment_creation_date")]
        [JsonPropertyName("appointment_creation_date")]
        public DateTime AppointmentCreationDate { get; set; }

        [JsonProperty("comments")]
        [JsonPropertyName("comments")]
        public string Comments { get; set; }
    }
}

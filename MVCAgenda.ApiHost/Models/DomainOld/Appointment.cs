using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MVCAgenda.ApiHost.Models.Domain
{
    public class Appointment : BaseEntityDto
    {
        [JsonProperty("medic_name")]
        [JsonPropertyName("medic_name")]
        public int PatientId { get; set; }

        [JsonProperty("room_id")]
        [JsonPropertyName("room_id")]
        public int RoomId { get; set; }

        [JsonProperty("medic_id")]
        [JsonPropertyName("medic_id")]
        public int MedicId { get; set; }

        [JsonProperty("appointment_date")]
        [JsonPropertyName("appointment_date")]
        public string AppointmentDate { get; set; }

        [JsonProperty("appointment_hour")]
        [JsonPropertyName("appointment_hour")]
        public string AppointmentHour { get; set; }

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
        public string AppointmentCreationDate { get; set; }

        [JsonProperty("comments")]
        [JsonPropertyName("comments")]
        public string Comments { get; set; }
    }
}
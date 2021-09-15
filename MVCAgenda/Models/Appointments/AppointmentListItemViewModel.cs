using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MVCAgenda.Models.BaseModels;

namespace MVCAgenda.Models.Appointments
{
    public class AppointmentListItemViewModel : BaseModel
    {
        [DisplayName("Pacient")]
        public int PatientId { get; set; }

        [DisplayName("Nume")]
        public string FirstName { get; set; }

        [DisplayName("Număr de telefon")]
        public string PhonNumber { get; set; }


        [DisplayName("Medic")]
        public string Medic { get; set; }

        [DisplayName("Camera")]
        public string Room { get; set; }


        [DisplayName("Data")]
        [DataType(DataType.Date)]
        public string AppointmentDate { get; set; }

        [DisplayName("Ora")]
        [DataType(DataType.Time)]
        public string AppointmentHour { get; set; }

        [DisplayName("Procedura")]
        public string Procedure { get; set; }
    }
}

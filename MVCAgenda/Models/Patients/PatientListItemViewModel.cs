using MVCAgenda.Models.BaseModels;
using System.ComponentModel;

namespace MVCAgenda.Models.Patients
{
    public class PatientListItemViewModel : BaseEntityModel
    {
        [DisplayName("Detalii pacient")]
        public int SheetPatientId { get; set; }

        [DisplayName("Nume")]
        public string FirstName { get; set; }

        [DisplayName("Prenume")]
        public string LastName { get; set; }

        [DisplayName("Număr de telefon")]
        public string PhoneNumber { get; set; }

        [DisplayName("E-mail")]
        public string Mail { get; set; }
    }
}

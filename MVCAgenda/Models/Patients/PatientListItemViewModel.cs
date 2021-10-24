using MVCAgenda.Models.BaseModels;
using System.ComponentModel;

namespace MVCAgenda.Models.Patients
{
    public class PatientListItemViewModel : BaseEntityModel
    {
        [DisplayName("Nume")]
        public string FirstName { get; set; }

        [DisplayName("Prenume")]
        public string LastName { get; set; }

        [DisplayName("Număr de telefon")]
        public string PhoneNumber { get; set; }

        [DisplayName("E-mail")]
        public string Mail { get; set; }

        [DisplayName("Tipul pacientului")]
        public int StatusCode { get; set; }
    }
}
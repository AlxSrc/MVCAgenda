using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Patients;

namespace MVCAgenda.Factories.Patients
{
    public interface IPatientsFactory
    {
        #region PreperePatientViewModel
        PatientViewModel PreperePatientViewModel(Patient patient);
        #endregion
    }
}

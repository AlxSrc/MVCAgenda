using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Patients;

namespace MVCAgenda.Factories.Patients
{
    public interface IPatientsFactory
    {
        PatientViewModel PreperePatientViewModel(Patient patient);
        PatientListItemViewModel PreperePatientListItemViewModel(Patient patient);
    }
}
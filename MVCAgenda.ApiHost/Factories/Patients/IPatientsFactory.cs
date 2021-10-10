using MVCAgenda.ApiHost.DTOs.Patients;
using MVCAgenda.Core.Domain;

namespace MVCAgenda.ApiHost.Factories.Patients
{
    public interface IPatientsFactory
    {
        PatientDto PreperePatientDTO(Patient patient);
    }
}
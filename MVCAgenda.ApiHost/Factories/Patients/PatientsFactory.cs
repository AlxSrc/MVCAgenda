using MVCAgenda.ApiHost.DTOs.Patients;
using MVCAgenda.Core.Domain;

namespace MVCAgenda.ApiHost.Factories.Patients
{
    public class PatientsFactory : IPatientsFactory
    {
        public PatientDto PreperePatientDTO(Patient patient)
        {
            PatientDto patientDTO = new PatientDto()
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                Mail = patient.Mail,
                StatusCode = patient.StatusCode
            };

            return patientDTO;
        }
    }
}
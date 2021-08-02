using MVCAgenda.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Patients
{
    public interface IPatientServices
    {
        Task<string> CreatePatientAsync(Patient PatientModel);
        Task<bool> EditPatientAsync(Patient PatientModel);
        Task<bool> DeletePatientAsync(int id);
        Task<bool> HidePatientAsync(int id);
        Task<List<Patient>> GetPatientAsync();
        Task<Patient> GetPatientByIdAsync(int Id);
    }
}

using MVCAgenda.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Patients
{
    public interface IPatientServices
    {
        Task<string> CreatePatientAsync(Core.Domain.Patient PatientModel);
        Task<bool> EditPatientAsync(Core.Domain.Patient PatientModel);
        Task<bool> DeletePatientAsync(int id);
        Task<bool> HidePatientAsync(int id);
        Task<List<Core.Domain.Patient>> GetPatientAsync();
        Task<Core.Domain.Patient> GetPatientByIdAsync(int Id);
    }
}

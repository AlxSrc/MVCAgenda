using System.Collections.Generic;
using System.Threading.Tasks;
using MVCAgenda.Core.Domain;

namespace MVCAgenda.Service.Patients
{
    public interface IPatientService
    {
        Task<bool> CreateAsync(Patient patient);
        Task<int> CheckExistentPatientAsync(Patient patient);
        
        Task<List<Patient>> GetListAsync(string searchByName, string searchByPhoneNumber, string searchByEmail, bool includeBlackList, bool isDeleted);
        Task<Patient> GetAsync(int Id, bool GetPatientByPatientSheetId = false);
        
        Task<bool> UpdateAsync(Patient patient);
        
        Task<bool> DeleteAsync(int id);
        Task<bool> HideAsync(int id);
    }
}

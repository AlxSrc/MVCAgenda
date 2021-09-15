using MVCAgenda.Models.Patients;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Patients
{
    public interface IPatientsManager
    {
        Task<string> CreateAsync(PatientViewModel patientViewModel);
        
        Task<PatientsViewModel> GetListAsync(string searchByName, string searchByPhoneNumber, string searchByEmail, bool includeBlackList, bool isDeleted);
        
        Task<PatientViewModel> GetDetailsAsync(int id);
       
        Task<string> UpdateAsync(PatientViewModel patientViewModel);
       
        Task<string> DeleteAsync(int id);
    }
}

using MVCAgenda.Core.Domain;
using MVCAgenda.Core.MVCAgendaManagement;
using MVCAgenda.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Patients
{
    public interface IPatientServices
    {
        Task<string> CreatePatientAsync(Patient PatientModel);
        Task<string> EditPatientAsync(Patient PatientModel);
        Task<bool> DeletePatientAsync(int id);
        Task<string> HidePatientAsync(int id);
        Task<MVCAgendaViewsManager> GetPatientAsync(string SearchByName, string SearchByPhoneNumber, string SearchByEmail, bool includeBlackList, bool isDeleted);
        Task<Patient> GetPatientByIdAsync(int Id);
        Task<PatientViewModel> GetPatientViewModelByIdAsync(Patient patient);
    }
}

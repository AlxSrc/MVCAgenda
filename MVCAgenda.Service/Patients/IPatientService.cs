using System.Collections.Generic;
using System.Threading.Tasks;
using MVCAgenda.Core.Domain;

namespace MVCAgenda.Service.Patients
{
    public interface IPatientService
    {
        Task<int> CreateAsync(Patient patient);
        Task<int> CheckExistentPatientAsync(Patient patient);

        Task<List<Patient>> GetListAsync(int pageIndex,
            string searchByName = null,
            string searchByPhoneNumber = null,
            string searchByEmail = null,
            bool? includeBlackList = null,
            bool? isDeleted = null);

        Task<int> GetPatientsNumberAsync(
            string searchByName = null,
            string searchByPhoneNumber = null,
            string searchByEmail = null,
            bool? includeBlackList = false,
            bool? isDeleted = false);

        Task<Patient> GetAsync(int Id);

        Task<bool> UpdateAsync(Patient patient);

        Task<bool> DeleteAsync(int id);
        Task<bool> UnHideAsync(int id);
        Task<bool> HideAsync(int id);
    }
}
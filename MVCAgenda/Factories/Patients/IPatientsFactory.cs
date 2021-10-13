using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Patients;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Patients
{
    public interface IPatientsFactory
    {
        Task<PatientViewModel> PrepereDetailsViewModelAsync(int id);

        Task<PatientsViewModel> GetListViewModelAsync(string searchByName = null, string searchByPhoneNumber = null, string searchByEmail = null, bool? includeBlackList = null, bool? isDeleted = null);
    }
}
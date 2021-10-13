using MVCAgenda.Models.Patients;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Patients
{
    public interface IPatientsManager
    {
        Task<string> CreateAsync(PatientViewModel patientViewModel);

        Task<string> UpdateAsync(PatientViewModel patientViewModel);

        Task<string> DeleteAsync(int id);
    }
}
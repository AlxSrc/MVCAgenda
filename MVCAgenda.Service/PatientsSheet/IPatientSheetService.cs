using MVCAgenda.Core.Domain;
using System.Threading.Tasks;

namespace MVCAgenda.Service.PatientsSheet
{
    public interface IPatientSheetService
    {
        Task<PatientSheet> GetAsync(int Id);

        Task<bool> UpdateAsync(PatientSheet patientSheet);
    }
}

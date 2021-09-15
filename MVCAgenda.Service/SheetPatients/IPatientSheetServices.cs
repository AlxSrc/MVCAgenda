using MVCAgenda.Core.Domain;
using System.Threading.Tasks;

namespace MVCAgenda.Service.SheetPatients
{
    public interface IPatientSheetServices
    {
        Task<PatientSheet> GetAsync(int Id);

        Task<bool> UpdateAsync(PatientSheet patientSheet);
    }
}

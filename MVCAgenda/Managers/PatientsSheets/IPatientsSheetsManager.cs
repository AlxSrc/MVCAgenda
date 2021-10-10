using MVCAgenda.Models.PatientSheets;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.PatientsSheets
{
    public interface IPatientsSheetsManager
    {
        Task<PatientSheetDetailsViewModel> GetDetailsAsync(int id);
        Task<PatientSheetEditViewModel> GetEditDetailsAsync(int id);
        Task<string> UpdateAsync(PatientSheetEditViewModel patientSheetViewModel);
    }
}
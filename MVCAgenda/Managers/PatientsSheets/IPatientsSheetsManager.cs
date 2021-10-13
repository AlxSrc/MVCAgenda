using MVCAgenda.Models.PatientSheets;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.PatientsSheets
{
    public interface IPatientsSheetsManager
    {
        Task<string> UpdateAsync(PatientSheetEditViewModel patientSheetViewModel);
    }
}
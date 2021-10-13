using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Consultations;
using MVCAgenda.Models.PatientSheets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.PatientsSheet
{
    public interface IPatientsSheetsFactory
    {
        Task<PatientSheetDetailsViewModel> PrepereDetailsViewModelAsync(int id);

        Task<PatientSheetEditViewModel> PrepereEditViewModelAsync(int id);
    }
}
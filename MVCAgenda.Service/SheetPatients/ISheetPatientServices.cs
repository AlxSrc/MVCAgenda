using MVCAgenda.Core.Domain;
using MVCAgenda.Core.ViewModels;
using System.Threading.Tasks;

namespace MVCAgenda.Service.SheetPatients
{
    public interface ISheetPatientServices
    {
        Task<string> EditSheetPatientAsync(SheetPatient SheetPatientModel);
        Task<SheetPatientViewModel> GetSheetPatientViewModelByIdAsync(int Id);
        Task<SheetPatient> GetSheetPatientByIdAsync(int Id);
    }
}

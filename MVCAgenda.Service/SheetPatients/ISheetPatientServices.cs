using MVCAgenda.Core.Domain;
using MVCAgenda.Core.ViewModels;
using System.Threading.Tasks;

namespace MVCAgenda.Service.SheetPatients
{
    public interface ISheetPatientServices
    {
        Task<string> EditSheetPatientAsync(SheetPatientViewModel SheetPatientModel);
        Task<SheetPatientViewModel> GetSheetPatientViewModelByIdAsync(int Id);
        Task<SheetPatientViewModel> GetSheetPatientByIdAsync(int Id);
    }
}

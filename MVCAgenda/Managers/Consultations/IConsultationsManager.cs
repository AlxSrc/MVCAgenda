using MVCAgenda.Models.Consultations;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Consultations
{
    public interface IConsultationsManager
    {
        Task<string> CreateAsync(ConsultationCreateViewModel consultationViewModel);

        Task<ConsultationDetailsViewModel> GetDetailsAsync(int id);
        Task<ConsultationEditViewModel> GetEditDetailsAsync(int id);

        Task<string> UpdateAsync(ConsultationEditViewModel consultationViewModel);

        Task<string> DeleteAsync(int id);
    }
}
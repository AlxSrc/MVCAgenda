using MVCAgenda.Core.Domain;
using MVCAgenda.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Consultations
{
    public interface IConsultationServices
    {
        Task<bool> CreateConsultationAsync(ConsultationViewModel consultationModel);
        Task<ConsultationViewModel> GetConsultationAsync(int id);
        Task<List<ConsultationViewModel>> GetConsultationsAsync(int id);
        Task<bool> EditConsultationAsync(ConsultationViewModel consultationModel);
        Task<bool> HideConsultationAsync(int id);
        Task<bool> DeleteConsultationAsync(int id);
    }
}

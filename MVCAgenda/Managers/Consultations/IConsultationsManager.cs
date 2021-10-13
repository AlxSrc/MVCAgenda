using MVCAgenda.Models.Consultations;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Consultations
{
    public interface IConsultationsManager
    {
        Task<string> CreateAsync(ConsultationCreateViewModel consultationViewModel);

        Task<string> UpdateAsync(ConsultationEditViewModel consultationViewModel);

        Task<string> DeleteAsync(int id);
    }
}
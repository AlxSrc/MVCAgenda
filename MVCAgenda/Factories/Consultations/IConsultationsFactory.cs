using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Consultations;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Consultations
{
    public interface IConsultationsFactory
    {
        Task<ConsultationViewModel> PrepereViewModelAsync(Consultation consultation);

        Task<ConsultationDetailsViewModel> PrepereDetailsViewModelAsync(int id);

        Task<ConsultationEditViewModel> PrepereEditViewModelAsync(int id);

        Task<ConsultationCreateViewModel> PrepereCreateViewModelAsync(int id);
    }
}
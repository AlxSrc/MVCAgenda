using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Consultations;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Consultations
{
    public interface IConsultationsFactory
    {
        Task<ConsultationViewModel> PrepereConsultationViewModel(Consultation consultation);

        Task<ConsultationDetailsViewModel> PrepereConsultationDetailsViewModel(Consultation consultation);
        Task<ConsultationEditViewModel> PrepereConsultationEditDetailsViewModel(Consultation consultation);
    }
}

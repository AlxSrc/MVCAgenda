using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Consultations;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Consultations
{
    public class ConsultationsFactory : IConsultationsFactory
    {
        public async Task<ConsultationViewModel> PrepereConsultationViewModel(Consultation consultation)
        {
            ConsultationViewModel consultationViewModel = new ConsultationViewModel()
            {
                Id = consultation.Id,
                SheetPatientId = consultation.SheetPatientId,
                Symptoms = consultation.Symptoms,
                Diagnostic = consultation.Diagnostic,
                Prescriptions = consultation.Prescriptions,
                CreationDate = consultation.CreationDate,
                Hidden = consultation.Hidden
            };
            return consultationViewModel;
        }

        public async Task<ConsultationDetailsViewModel> PrepereConsultationDetailsViewModel(Consultation consultation)
        {
            var consultationViewModel = new ConsultationDetailsViewModel()
            {
                Id = consultation.Id,
                SheetPatientId = consultation.SheetPatientId,
                Symptoms = consultation.Symptoms,
                Diagnostic = consultation.Diagnostic,
                Prescriptions = consultation.Prescriptions,
                CreationDate = consultation.CreationDate,
                Hidden = consultation.Hidden
            };
            return consultationViewModel;
        }
        public async Task<ConsultationEditViewModel> PrepereConsultationEditDetailsViewModel(Consultation consultation)
        {
            var consultationViewModel = new ConsultationEditViewModel()
            {
                Id = consultation.Id,
                SheetPatientId = consultation.SheetPatientId,
                Symptoms = consultation.Symptoms,
                Diagnostic = consultation.Diagnostic,
                Prescriptions = consultation.Prescriptions,
                CreationDate = consultation.CreationDate,
                Hidden = consultation.Hidden
            };
            return consultationViewModel;
        }
    }
}

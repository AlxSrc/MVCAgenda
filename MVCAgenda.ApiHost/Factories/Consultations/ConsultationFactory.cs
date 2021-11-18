using MVCAgenda.ApiHost.DTOs.Consultations;
using MVCAgenda.Core.Domain;

namespace MVCAgenda.ApiHost.Factories.Consultations
{
    public class ConsultationFactory: IConsultationFactory
    {
        public ConsultationDto PrepereConsultationDTO(Consultation consultation)
        {
            return new ConsultationDto() 
            { 
                Id = consultation.Id,
                PatientSheetId = consultation.PatientSheetId,
                Prescriptions = consultation.Prescriptions,
                Symptoms = consultation.Symptoms,
                CreationDate = consultation.CreationDate,
                Diagnostic = consultation.Diagnostic,
                Hidden = consultation.Hidden
            };
        }
    }
}

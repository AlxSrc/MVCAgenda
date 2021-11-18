using MVCAgenda.ApiHost.DTOs.Consultations;
using MVCAgenda.Core.Domain;
using System.Threading.Tasks;

namespace MVCAgenda.ApiHost.Factories.Consultations
{
    public interface IConsultationFactory
    {
        ConsultationDto PrepereConsultationDTO(Consultation consultation);
    }
}

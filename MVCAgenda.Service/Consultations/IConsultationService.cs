using System.Collections.Generic;
using System.Threading.Tasks;
using MVCAgenda.Core.Domain;

namespace MVCAgenda.Service.Consultations
{
    public interface IConsultationService
    {
        Task<bool> CreateAsync(Consultation consultation);

        Task<Consultation> GetAsync(int id);
        Task<List<Consultation>> GetListAsync(int id);

        Task<bool> UpdateAsync(Consultation consultation);

        Task<bool> HideAsync(int id);
        Task<bool> UnHideAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
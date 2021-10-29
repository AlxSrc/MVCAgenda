using MVCAgenda.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Medics
{
    public interface IMedicService
    {
        Task<bool> CreateAsync(Medic medic);

        Task<Medic> GetAsync(int id);
        Task<Medic> GetAsync(string mail);
        Task<List<Medic>> GetListAsync(string mail = null, bool? hidden = null);

        Task<bool> UpdateAsync(Medic medic);

        Task<bool> HideAsync(int id);
        Task<bool> UnHideAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
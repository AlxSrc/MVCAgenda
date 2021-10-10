using MVCAgenda.Models.Medics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Medics
{
    public interface IMedicsManager
    {
        Task<string> CreateAsync(MedicViewModel model);

        Task<List<MedicViewModel>> GetListAsync();
        Task<MedicViewModel> GetDetailsAsync(int id);

        Task<string> UpdateAsync(MedicViewModel model);

        Task<string> DeleteAsync(int id);
    }
}
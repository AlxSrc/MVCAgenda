using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Medics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Medics
{
    public interface IMedicsFactory
    {
        Task<List<MedicViewModel>> PrepereListModel(string mail = null, bool? hidden = null);
        Task<MedicViewModel> PrepereDetailsViewModel(int id);
        Task<MedicsViewModel> PrepereMedicsListViewModelAsync();
    }
}
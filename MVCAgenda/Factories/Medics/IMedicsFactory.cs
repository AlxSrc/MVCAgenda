using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Medics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Medics
{
    public interface IMedicsFactory
    {
        Task<List<MedicViewModel>> PrepereListModel();
        Task<MedicViewModel> PrepereDetailsViewModel(int id);
        Task<MedicsViewModel> PrepereMedicsListViewModelAsync();
    }
}
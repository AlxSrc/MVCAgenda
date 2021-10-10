using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Medics;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Medics
{
    public interface IMedicsFactory
    {
        Task<MedicViewModel> PrepereMedicViewModel(Medic medic);
    }
}
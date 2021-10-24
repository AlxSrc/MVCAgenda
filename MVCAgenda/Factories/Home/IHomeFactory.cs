using MVCAgenda.Models.Home;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Home
{
    public interface IHomeFactory
    {
        Task<HomeViewModel> PrepereHomeViewModel(string mail = null);
    }
}

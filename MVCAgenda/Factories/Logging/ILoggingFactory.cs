using MVCAgenda.Core.Logging;
using MVCAgenda.Models.Logging;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Logging
{
    public interface ILoggingFactory
    {
        Task<LogsViewModel> GetViewModel();
    }
}
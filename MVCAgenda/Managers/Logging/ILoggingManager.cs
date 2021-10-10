using MVCAgenda.Models.Logging;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Logging
{
    public interface ILoggingManager
    {
        Task<LogsViewModel> GetLogsListViewModel();
    }
}
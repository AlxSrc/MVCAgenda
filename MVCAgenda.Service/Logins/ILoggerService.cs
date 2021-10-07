using MVCAgenda.Core.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Logins
{
    public interface ILoggerService
    {
        Task<bool> CreateAsync(Log log);
        Task<bool> CreateAsync(string message, string fullMessage, string ipAdress, LogLevel logLevel);

        Task<Log> GetAsync(int id);
        Task<List<Log>> GetListAsync();

        Task<bool> UpdateAsync(Log log);

        Task<bool> DeleteAsync(int id);
    }
}

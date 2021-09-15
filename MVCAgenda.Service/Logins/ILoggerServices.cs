using MVCAgenda.Core.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Logins
{
    public interface ILoggerServices
    {
        Task<bool> CreateAsync(Log log);
        
        Task<Log> GetAsync(int id);
        Task<List<Log>> GetListAsync();

        Task<bool> UpdateAsync(Log log);

        Task<bool> HideAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}

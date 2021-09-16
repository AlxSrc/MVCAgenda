using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Logging;
using MVCAgenda.Data.DataBaseManager;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Logins
{
    public class LoggerService : ILoggerService
    {
        #region Fields
        private readonly AgendaContext _context;
        #endregion
        /**************************************************************************************/
        #region Constructor
        public LoggerService(AgendaContext context)
        {
            _context = context;
        }
        #endregion
        /**************************************************************************************/
        #region Methods
        public async Task<bool> CreateAsync(Log log)
        {
            try
            {
                _context.Add(log);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        
        public async Task<Log> GetAsync(int id)
        {
            return await _context.Logs.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<List<Log>> GetListAsync()
        {
            return await _context.Logs.OrderBy(l => l.CreatedOnUtc).Where(l => l.Hidden == false).ToListAsync();
        }
        
        public async Task<bool> UpdateAsync(Log log)
        {
            try
            {
                _context.Update(log);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
       
        public async Task<bool> HideAsync(int id)
        {
            try
            {
                var log = await _context.Logs.FindAsync(id);
                log.Hidden = true;
                _context.Logs.Update(log);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _context.Logs.Remove(await _context.Logs.FindAsync(id));
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}

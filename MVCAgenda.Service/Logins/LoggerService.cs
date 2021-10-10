using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Logging;
using MVCAgenda.Data.DataBaseManager;
using System;
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
                log.CreatedOnUtc = DateTime.UtcNow;
                _context.Add(log);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateAsync(string message, string fullMessage, string ipAdress, LogLevel logLevel)
        {
            try
            {
                var log = new Log()
                {
                    ShortMessage = message,
                    FullMessage = fullMessage,
                    IpAddress = ipAdress,
                    LogLevel = logLevel,
                    CreatedOnUtc = DateTime.UtcNow,
                    Hidden = false
                };
                _context.Add(log);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Log> GetAsync(int id)
        {
            try
            {
                return await _context.Logs.FirstOrDefaultAsync(m => m.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Log>> GetListAsync()
        {
            try
            {
                return await _context.Logs.OrderByDescending(l => l.CreatedOnUtc).ToListAsync();
            }
            catch
            {
                return null;
            }
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
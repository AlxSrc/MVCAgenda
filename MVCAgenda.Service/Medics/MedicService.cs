using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Core.Logging;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Logins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Medics
{
    public class MedicService : IMedicService
    {
        private string user = "TestUser";

        #region Fields

        private string msg;
        private readonly AgendaContext _context;
        private readonly ILoggerService _logger;

        #endregion

        /**************************************************************************************/

        #region Constructor

        public MedicService(AgendaContext context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        #endregion

        /**************************************************************************************/

        #region Methods

        public async Task<bool> CreateAsync(Medic medic)
        {
            try
            {
                _context.Add(medic);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Medics}, Action: {LogInfo.Create}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return false;
            }
        }

        public async Task<Medic> GetAsync(int id)
        {
            try
            {
                return await _context.Medics.FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Medics}, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new Medic();
            }
        }
        public async Task<Medic> GetAsync(string mail)
        {
            try
            {
                return await _context.Medics.FirstOrDefaultAsync(m => m.Mail == mail);
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Medics}, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new Medic();
            }
        }

        public async Task<List<Medic>> GetListAsync(string mail = null)
        {
            try
            {
                var query = _context.Medics.AsQueryable();

                query = query.Where(m => m.Hidden == false);

                //query = query.Where(m => m.Mail != Constants.AdminUser);

                if (mail != null)
                    query = query.Where(m => m.Mail.ToUpper().Contains(mail.ToUpper()));

                query = query.OrderBy(m => m.Name);

                return await query.ToListAsync();
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Medics}, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Medic medic)
        {
            try
            {
                _context.Update(medic);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Medics}, Action: {LogInfo.Edit}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return false;
            }
        }

        public async Task<bool> HideAsync(int id)
        {
            try
            {
                var medic = await _context.Medics.FindAsync(id);
                medic.Hidden = true;
                _context.Medics.Update(medic);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Medics}, Action: {LogInfo.Hide}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _context.Medics.Remove(await _context.Medics.FindAsync(id));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Medics}, Action: {LogInfo.Delete}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return false;
            }
        }

        #endregion
    }
}
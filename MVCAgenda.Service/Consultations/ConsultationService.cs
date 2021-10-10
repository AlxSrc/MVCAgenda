using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Logging;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Logins;

namespace MVCAgenda.Service.Consultations
{
    public class ConsultationService : IConsultationService
    {
        private string user = "TestUser";

        #region Fields

        private string msg;
        private readonly AgendaContext _context;
        private readonly ILoggerService _logger;

        #endregion

        /**************************************************************************************/

        #region Constructor

        public ConsultationService(AgendaContext context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        #endregion

        /**************************************************************************************/

        #region Methods

        public async Task<bool> CreateAsync(Consultation consultation)
        {
            try
            {
                _context.Add(consultation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                msg = $"User: {user}, Table:{LogTable.Consultations}, Action: {LogInfo.Create}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return false;
            }
        }

        public async Task<Consultation> GetAsync(int id)
        {
            try
            {
                return await _context.Consultations.FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception ex)
            {
                msg = $"User: {user}, Table:{LogTable.Consultations}, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return new Consultation();
            }
        }

        public async Task<List<Consultation>> GetListAsync(int id)
        {
            try
            {
                var query = _context.Consultations.Where(p => p.SheetPatientId == id);
                return await query.OrderByDescending(c => c.CreationDate).Where(c => c.Hidden == false).ToListAsync();
            }
            catch (Exception ex)
            {
                msg = $"User: {user}, Table:{LogTable.Consultations}, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return new List<Consultation>();
            }
        }

        public async Task<bool> UpdateAsync(Consultation consultation)
        {
            try
            {
                _context.Update(consultation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                msg = $"User: {user}, Table:{LogTable.Consultations}, Action: {LogInfo.Edit}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return false;
            }
        }

        public async Task<bool> HideAsync(int id)
        {
            try
            {
                var consultation = await _context.Consultations.FindAsync(id);
                consultation.Hidden = true;
                _context.Consultations.Update(consultation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                msg = $"User: {user}, Table:{LogTable.Consultations}, Action: {LogInfo.Hide}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _context.Consultations.Remove(await _context.Consultations.FindAsync(id));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                msg = $"User: {user}, Table:{LogTable.Consultations}, Action: {LogInfo.Delete}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return false;
            }
        }

        #endregion
    }
}
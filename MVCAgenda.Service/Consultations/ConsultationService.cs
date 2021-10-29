using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Logging;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Logins;

namespace MVCAgenda.Service.Consultations
{
    public class ConsultationService : IConsultationService
    {
        #region Fields

        private string msg;
        private readonly AgendaContext _context;
        private readonly ILoggerService _logger; 
        private readonly IWorkContext _workContext;

        #endregion

        /**************************************************************************************/

        #region Constructor

        public ConsultationService(AgendaContext context, ILoggerService logger, IWorkContext workContext)
        {
            _context = context;
            _logger = logger;
            _workContext = workContext;
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
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Consultations}, Action: {LogInfo.Create}";
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
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Consultations}, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return new Consultation();
            }
        }

        public async Task<List<Consultation>> GetListAsync(int id)
        {
            try
            {
                if(id != -1)
                {
                    var query = _context.Consultations.Where(p => p.PatientSheetId == id);
                    return await query.OrderByDescending(c => c.CreationDate).Where(c => c.Hidden == false).ToListAsync();
                }
                else
                {
                    var query = _context.Consultations;
                    return await query.ToListAsync();
                }
                
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Consultations}, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return new List<Consultation>();
            }
        }

        public async Task<bool> UpdateAsync(Consultation consultation)
        {
            try
            {
                var consultationToBeEdited = await _context.Consultations.FirstOrDefaultAsync(c => c.Id == consultation.Id);
                _context.Entry(consultationToBeEdited).CurrentValues.SetValues(consultation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Consultations}, Action: {LogInfo.Edit}";
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
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Consultations}, Action: {LogInfo.Hide}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return false;
            }
        }

        public async Task<bool> UnHideAsync(int id)
        {
            try
            {
                var consultation = await _context.Consultations.FindAsync(id);
                consultation.Hidden = false;
                _context.Consultations.Update(consultation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Consultations}, Action: {LogInfo.UnHide}";
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
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Consultations}, Action: {LogInfo.Delete}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return false;
            }
        }

        #endregion
    }
}
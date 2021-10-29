using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Logging;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Logins;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Service.PatientsSheet
{
    public class PatientSheetService : IPatientSheetService
    {
        #region Fields

        private string msg;
        private readonly AgendaContext _context;
        private readonly ILoggerService _logger;
        private readonly IWorkContext _workContext;

        #endregion

        /**************************************************************************************/

        #region Constructor

        public PatientSheetService(AgendaContext context, ILoggerService logger, IWorkContext workContext)
        {
            _context = context;
            _logger = logger;
            _workContext = workContext;
        }

        #endregion

        /**************************************************************************************/

        #region Methods

        public async Task<bool> CreateAsync(PatientSheet patientSheet)
        {
            try
            {
                _context.Add(patientSheet);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.PatientSheets}, Action: {LogInfo.Create}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return false;
            }
        }

        public async Task<PatientSheet> GetAsync(int Id)
        {
            try
            {
                return await _context.PatientSheets.FirstOrDefaultAsync(m => m.Id == Id);
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.PatientSheets}, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return null;
            }
        }

        public async Task<List<PatientSheet>> GetListAsync()
        {
            return await _context.PatientSheets.ToListAsync();
        }

        public async Task<bool> UpdateAsync(PatientSheet patientSheet)
        {
            try
            {
                var patientSheetToBeEdited = await _context.PatientSheets.FirstOrDefaultAsync(p => p.Id == patientSheet.Id);
                _context.Entry(patientSheetToBeEdited).CurrentValues.SetValues(patientSheet);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.PatientSheets}, Action: {LogInfo.Edit}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return false;
            }
        }

        #endregion
    }
}
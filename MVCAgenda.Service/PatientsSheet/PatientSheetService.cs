using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Logging;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Logins;
using System;
using System.Threading.Tasks;

namespace MVCAgenda.Service.PatientsSheet
{
    public class PatientSheetService : IPatientSheetService
    {
        private string user = "TestUser";

        #region Fields

        private string msg;
        private readonly AgendaContext _context;
        private readonly ILoggerService _logger;

        #endregion

        /**************************************************************************************/

        #region Constructor

        public PatientSheetService(AgendaContext context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        #endregion

        /**************************************************************************************/

        #region Methods

        public async Task<PatientSheet> GetAsync(int Id)
        {
            try
            {
                return await _context.PatientsSheet.FirstOrDefaultAsync(m => m.Id == Id);
            }
            catch (Exception ex)
            {
                msg = $"User: {user}, Table:{LogTable.PatientSheets}, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return null;
            }
        }

        public async Task<bool> UpdateAsync(PatientSheet patientSheet)
        {
            try
            {
                _context.Update(patientSheet);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                msg = $"User: {user}, Table:{LogTable.PatientSheets}, Action: {LogInfo.Edit}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return false;
            }
        }

        #endregion
    }
}
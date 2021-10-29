using MVCAgenda.Core.Helpers;
using MVCAgenda.Factories.Logging;
using MVCAgenda.Models.Logging;
using MVCAgenda.Service.Logins;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Logging
{
    public class LoggingManager : ILoggingManager
    {
        #region Fields

        private readonly ILoggerService _logServices;

        #endregion

        /**********************************************************************************/

        #region Constructor

        public LoggingManager(ILoggerService logServices)
        {
            _logServices = logServices;
        }

        #endregion

        /**********************************************************************************/

        #region Methods
        public async Task<string> DeleteAllAsync()
		{
			try
			{
                var logs = await _logServices.GetListAsync();
                foreach (var log in logs)
                    await _logServices.DeleteAsync(log.Id);

                return StringHelpers.SuccesMessage;
			}
            catch (Exception ex)
			{
                return null;
			}
		}
        #endregion
    }
}
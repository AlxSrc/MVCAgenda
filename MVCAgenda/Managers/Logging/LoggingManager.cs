using MVCAgenda.Factories.Logging;
using MVCAgenda.Models.Logging;
using MVCAgenda.Service.Logins;
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

        #endregion
    }
}
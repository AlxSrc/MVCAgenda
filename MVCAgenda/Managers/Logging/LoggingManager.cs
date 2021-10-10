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
        private readonly ILoggingFactory _logFactory;

        #endregion

        /**********************************************************************************/

        #region Constructor

        public LoggingManager(ILoggerService logServices, ILoggingFactory logFactory)
        {
            _logServices = logServices;
            _logFactory = logFactory;
        }

        #endregion

        /**********************************************************************************/

        #region Methods

        #endregion

        public async Task<LogsViewModel> GetLogsListViewModel()
        {
            try
            {
                var logsViewModel = new List<LogListItemViewModel>();
                var logs = await _logServices.GetListAsync();

                foreach (var log in logs)
                    logsViewModel.Add(_logFactory.PrepereLogViewModel(log));

                return new LogsViewModel() { Logs = logsViewModel };
            }
            catch
            {
                return new LogsViewModel() { Logs = new List<LogListItemViewModel>() };
            }
        }
    }
}
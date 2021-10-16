using MVCAgenda.Core.Logging;
using MVCAgenda.Models.Logging;
using MVCAgenda.Service.Logins;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Logging
{
    public class LoggingFactory : ILoggingFactory
    {
        #region Fields

        private readonly ILoggerService _logServices;

        #endregion

        /**********************************************************************************/

        #region Constructor

        public LoggingFactory(ILoggerService logServices)
        {
            _logServices = logServices;
        }

        #endregion

        /**********************************************************************************/

        #region Methods

        public async Task<LogsViewModel> PrepereLogsViewModel()
        {
            try
            {
                var logsViewModel = new List<LogListItemViewModel>();
                var logs = await _logServices.GetListAsync();

                foreach (var log in logs)
                    logsViewModel.Add(PrepereLogListItemViewModel(log));

                return new LogsViewModel() { Logs = logsViewModel };
            }
            catch
            {
                return new LogsViewModel() { Logs = new List<LogListItemViewModel>() };
            }
        }

        public async Task<LogListItemViewModel> PrepereDetailsViewModel(int id)
        {
            try
            {
                return PrepereLogListItemViewModel(await _logServices.GetAsync(id));
            }
            catch
            {
                return new LogListItemViewModel();
            }
        }

        #endregion

        /**********************************************************************************/

        #region Utils
        public static LogListItemViewModel PrepereLogListItemViewModel(Log log)
        {
            LogListItemViewModel viewModel = new LogListItemViewModel()
            {
                Id = log.Id,
                IpAddress = log.IpAddress,
                ShortMessage = log.ShortMessage,
                FullMessage = log.FullMessage,
                CreatedOnUtc = log.CreatedOnUtc
            };

            switch (log.LogLevel)
            {
                case LogLevel.Information:
                    viewModel.LogLevel = "<span class=\"badge rounded-pill bg-info\">Action</span>";
                    break;
                case LogLevel.Error:
                    viewModel.LogLevel = "<span class=\"badge rounded-pill bg-danger\">Eroare</span>";
                    break;
                case LogLevel.Fatal:
                    viewModel.LogLevel = "<span class=\"badge rounded-pill bg-dark\">Fatal</span>";
                    break;
                default:
                    viewModel.LogLevel = "<span class=\"badge rounded-pill bg-info text-dark\">Info</span>";
                    break;
            }

            return viewModel;
        }
        #endregion
    }
}
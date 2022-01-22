using MVCAgenda.Core.Logging;
using MVCAgenda.Models.Logging;
using MVCAgenda.Service.Helpers;
using MVCAgenda.Service.Logins;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Logging
{
    public class LoggingFactory : ILoggingFactory
    {
        #region Fields

        private readonly ILoggerService _logServices;
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        /**********************************************************************************/

        #region Constructor

        public LoggingFactory(ILoggerService logServices,
            IDateTimeHelper dateTimeHelper)
        {
            _logServices = logServices;
            _dateTimeHelper = dateTimeHelper;
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
                    logsViewModel.Add(await PrepereLogListItemViewModel(log));

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
                var log = await _logServices.GetAsync(id);
                var model = log;
                model.CreatedOnUtc = await _dateTimeHelper.ConvertToUserTimeAsync(log.CreatedOnUtc);
                return await PrepereLogListItemViewModel(model);
            }
            catch
            {
                return new LogListItemViewModel();
            }
        }

        #endregion

        /**********************************************************************************/

        #region Utils
        public async Task<LogListItemViewModel> PrepereLogListItemViewModel(Log log)
        {
            LogListItemViewModel viewModel = new LogListItemViewModel()
            {
                Id = log.Id,
                IpAddress = log.IpAddress,
                ShortMessage = log.ShortMessage,
                FullMessage = log.FullMessage,
                CreatedOnUtc = await _dateTimeHelper.ConvertToUserTimeAsync(log.CreatedOnUtc)
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
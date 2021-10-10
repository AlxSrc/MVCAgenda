using MVCAgenda.Core.Logging;
using MVCAgenda.Models.Logging;

namespace MVCAgenda.Factories.Logging
{
    public class LoggingFactory : ILoggingFactory
    {
        public LogListItemViewModel PrepereLogViewModel(Log log)
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
    }
}
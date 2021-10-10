using System.Collections.Generic;

namespace MVCAgenda.Models.Logging
{
    public class LogsViewModel
    {
        public LogsViewModel()
        {
            Logs = new List<LogListItemViewModel>();
        }

        public List<LogListItemViewModel> Logs { get; set; }
    }
}
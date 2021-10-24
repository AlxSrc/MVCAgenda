using MVCAgenda.Core.Logging;
using MVCAgenda.Models.Home;
using MVCAgenda.Service.Logins;
using System;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Home
{
    public class HomeFactory : IHomeFactory
    {
        string user = "admin";

        #region Fields

        private readonly ILoggerService _logger;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public HomeFactory(ILoggerService loggerServices)
        {
            _logger = loggerServices;
        }

        #endregion

        /***********************************************************************************/

        #region Methods

        public async Task<HomeViewModel> PrepereHomeViewModel(string mail = null)
        {
            try
            {
                var homeViewModel = new HomeViewModel();

                return null;
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Medics} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new HomeViewModel();
            }
        }

        #endregion

        /***********************************************************************************/

        #region Utils

        

        #endregion

        
    }
}

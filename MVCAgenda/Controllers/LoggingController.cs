using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Managers.Logging;
using MVCAgenda.Models.Logging;
using System.Threading.Tasks;

namespace MVCAgenda.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LoggingController : Controller
    {
        #region Fields
        private readonly ILoggingManager _loggingManager;
        #endregion
        /******************************************************************************************/
        #region Constructor
        public LoggingController(ILoggingManager loggingManager)
        {
            _loggingManager = loggingManager;
        }
        #endregion
        /******************************************************************************************/
        #region Methods
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _loggingManager.GetLogsListViewModel());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion
        
    }
}

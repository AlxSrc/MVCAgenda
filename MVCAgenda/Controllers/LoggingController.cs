using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Factories.Logging;
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
        private readonly ILoggingFactory _loggingFactory;

        #endregion

        /******************************************************************************************/

        #region Constructor

        public LoggingController(ILoggingManager loggingManager, ILoggingFactory loggingFactory)
        {
            _loggingManager = loggingManager;
            _loggingFactory = loggingFactory;
        }

        #endregion

        /******************************************************************************************/

        #region Methods

        public async Task<IActionResult> Index()
        {
            return View(await _loggingFactory.PrepereLogsViewModel());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _loggingFactory.PrepereDetailsViewModel(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed()
        {
            var result = await _loggingManager.DeleteAllAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
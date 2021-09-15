using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCAgenda.Core.Domain;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Managers.Medics;
using MVCAgenda.Managers.Rooms;
using MVCAgenda.Models.Accounts;
using MVCAgenda.Models.Home;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Controllers
{
    public class HomeController : Controller
    {
        #region Fields
        private readonly IRoomsManager _roomsManager;
        private readonly IMedicsManager _medicsManager;
        #endregion
        /**************************************************************************************/
        #region Constructor
        public HomeController(IRoomsManager roomsManager, IMedicsManager medicsManager)
        {
            _roomsManager = roomsManager;
            _medicsManager = medicsManager;
        }
        #endregion
        /**************************************************************************************/
        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion
        /**************************************************************************************/
        #region Manage
        public async Task<IActionResult> Manage()
        {
            if (User.Identity.IsAuthenticated)
            {
                var medics = await _medicsManager.GetListAsync();
                var rooms = await _roomsManager.GetListAsync();

                var model = new ManageViewModel
                {
                    Medics = medics,
                    Rooms = rooms
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion
        /**************************************************************************************/
        #region Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Factories.Medics;
using MVCAgenda.Factories.Rooms;
using MVCAgenda.Managers.Medics;
using MVCAgenda.Managers.Rooms;
using MVCAgenda.Models.Accounts;
using MVCAgenda.Models.Home;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MVCAgenda.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageController : Controller
    {
        #region Fields

        private readonly IRoomsManager _roomsManager;
        private readonly IRoomsFactory _roomsFactory;
        private readonly IMedicsManager _medicsManager;
        private readonly IMedicsFactory _medicsFactory;

        #endregion

        /**************************************************************************************/

        #region Constructor

        public ManageController(IRoomsManager roomsManager,
            IRoomsFactory roomsFactory,
            IMedicsManager medicsManager,
            IMedicsFactory medicsFactory)
        {
            _roomsManager = roomsManager;
            _roomsFactory = roomsFactory;
            _medicsManager = medicsManager;
            _medicsFactory = medicsFactory;
        }

        #endregion

        /**************************************************************************************/

        #region Manage

        public async Task<IActionResult> Manage()
        {
            var medics = await _medicsFactory.PrepereListModel();
            var rooms = await _roomsFactory.PrepereListViewModelAsync();

            var model = new ManageViewModel
            {
                Medics = medics,
                Rooms = rooms
            };

            return View(model);
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

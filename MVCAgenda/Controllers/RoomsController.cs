using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Factories.Rooms;
using MVCAgenda.Managers.Rooms;
using MVCAgenda.Models.Rooms;

namespace MVCAgenda.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        #region Fields

        private readonly IRoomsManager _roomManager;
        private readonly IRoomsFactory _roomFactory;

        #endregion

        /**************************************************************************************/

        #region Constructors

        public RoomsController(IRoomsManager roomManager, IRoomsFactory roomFactory)
        {
            _roomManager = roomManager;
            _roomFactory = roomFactory;
        }

        #endregion

        /**************************************************************************************/

        #region Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomViewModel room)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var result = await _roomManager.CreateAsync(room);
                    return RedirectToAction("Manage", "Manage");
                }

                return View(room);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        #endregion

        /**************************************************************************************/

        #region Read

        public async Task<IActionResult> Details(int id)
        {
            return View(await _roomFactory.PrepereDetailsViewModelAsync(id));
        }

        #endregion

        /**************************************************************************************/

        #region Update

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _roomFactory.PrepereDetailsViewModelAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomViewModel room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _roomManager.UpdateAsync(room);
                return RedirectToAction("Manage", "Manage");
            }

            return View(room);
        }

        #endregion

        /**************************************************************************************/

        #region Delete

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resuult = await _roomManager.DeleteAsync(id);
            return RedirectToAction("Manage", "Manage");
        }

        #endregion
    }
}
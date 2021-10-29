using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Core.Helpers;
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
            if (ModelState.IsValid)
            {
                var result = await _roomManager.CreateAsync(room);
                return RedirectToAction("Index", "Rooms");
            }

            return View(room);
        }

        #endregion

        /**************************************************************************************/

        #region Read
        public async Task<IActionResult> Index()
        {
            return View(await _roomFactory.PrepereRoomsViewModelAsync());

        }
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
                var ressult = await _roomManager.UpdateAsync(room);

                if (ressult == StringHelpers.SuccesMessage)
                    return RedirectToAction("Index", "Rooms");
                else
                    ModelState.AddModelError(string.Empty, ressult);
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
            return RedirectToAction("Index", "Rooms");
        }

        #endregion
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Managers.Rooms;
using MVCAgenda.Models.Rooms;

namespace MVCAgenda.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        #region Fields

        private readonly IRoomsManager _roomManager;

        #endregion

        /**************************************************************************************/

        #region Constructors

        public RoomsController(IRoomsManager roomManager)
        {
            _roomManager = roomManager;
        }

        #endregion

        /**************************************************************************************/

        #region Create

        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
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
                    return RedirectToAction("Manage", "Home");
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
            if (User.Identity.IsAuthenticated)
            {
                return View(await _roomManager.GetDetailsAsync(id));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        #endregion

        /**************************************************************************************/

        #region Update

        public async Task<IActionResult> Edit(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _roomManager.GetDetailsAsync(id));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomViewModel room)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != room.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    await _roomManager.UpdateAsync(room);
                    return RedirectToAction("Manage", "Home");
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

        #region Delete

        public async Task<IActionResult> Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _roomManager.GetDetailsAsync(id));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var resuult = await _roomManager.DeleteAsync(id);
                return RedirectToAction("Manage", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        #endregion
    }
}
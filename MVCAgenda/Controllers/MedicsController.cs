using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Managers.Medics;
using MVCAgenda.Models.Medics;

namespace MVCAgenda.Controllers
{
    public class MedicsController : Controller
    {
        #region Fields
        private readonly IMedicsManager _medicsManager;
        #endregion
        /**************************************************************************************/
        #region Constructors
        public MedicsController(IMedicsManager medicsManager)
        {
            _medicsManager = medicsManager;
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
        public async Task<IActionResult> Create(MedicViewModel medic)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var result = await _medicsManager.CreateAsync(medic);
                    if(result == StringHelpers.SuccesMessage)
                        return RedirectToAction("Manage", "Home");
                    else
                        ModelState.AddModelError(string.Empty, result);
                }
                return View(medic);
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
                return View(await _medicsManager.GetDetailsAsync(id));
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
                return View(await _medicsManager.GetDetailsAsync(id));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MedicViewModel medic)
        {
            if (User.Identity.IsAuthenticated)
            {

                if (ModelState.IsValid)
                {
                    var result = await _medicsManager.UpdateAsync(medic);
                    if (result == StringHelpers.SuccesMessage)
                        return RedirectToAction("Manage", "Home");
                    else
                        ModelState.AddModelError(string.Empty, result);
                }
                return View(medic);
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
                return View(await _medicsManager.GetDetailsAsync(id));
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
                var result = await _medicsManager.DeleteAsync(id);
                if (result == StringHelpers.SuccesMessage)
                    return RedirectToAction("Manage", "Home");
                else
                {
                    ModelState.AddModelError(string.Empty, result);
                    return RedirectToAction("Delete", "Medics", new { id = id });
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion
    }
}

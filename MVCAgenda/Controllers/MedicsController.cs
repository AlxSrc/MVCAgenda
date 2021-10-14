using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Factories.Medics;
using MVCAgenda.Managers.Medics;
using MVCAgenda.Models.Medics;

namespace MVCAgenda.Controllers
{
    [Authorize]
    public class MedicsController : Controller
    {
        #region Fields

        private readonly IMedicsManager _medicsManager;
        private readonly IMedicsFactory _medicsFactory;

        #endregion

        /**************************************************************************************/

        #region Constructors

        public MedicsController(IMedicsManager medicsManager, IMedicsFactory medicsFactory)
        {
            _medicsManager = medicsManager;
            _medicsFactory = medicsFactory;
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
            if (ModelState.IsValid)
            {
                var result = await _medicsManager.CreateAsync(medic);
                if (result == StringHelpers.SuccesMessage)
                    return RedirectToAction("Manage", "Manage");
                else
                    ModelState.AddModelError(string.Empty, result);
            }

            return View(medic);
        }

        #endregion

        /**************************************************************************************/

        #region Read

        public async Task<IActionResult> Index()
        {
            return View(await _medicsFactory.PrepereMedicsListViewModelAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _medicsFactory.PrepereDetailsViewModel(id));
        }

        #endregion

        /**************************************************************************************/

        #region Update

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _medicsFactory.PrepereDetailsViewModel(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MedicViewModel medic)
        {
            if (ModelState.IsValid)
            {
                var result = await _medicsManager.UpdateAsync(medic);
                if (result == StringHelpers.SuccesMessage)
                    return RedirectToAction("Manage", "Manage");
                else
                    ModelState.AddModelError(string.Empty, result);
            }

            return View(medic);
        }

        #endregion

        /**************************************************************************************/

        #region Delete

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _medicsManager.DeleteAsync(id);
            if (result == StringHelpers.SuccesMessage)
                return RedirectToAction("Manage", "Manage");
            else
            {
                ModelState.AddModelError(string.Empty, result);
                return RedirectToAction("Details", "Medics", new { id = id });
            }
        }

        #endregion
    }
}
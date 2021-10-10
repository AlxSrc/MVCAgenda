using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Factories.Patients;
using MVCAgenda.Managers.Patients;
using MVCAgenda.Models.Patients;

namespace MVCAgenda.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        #region Fields

        private readonly IPatientsManager _patientManager;

        #endregion

        /*********************************************************************************/

        #region Constructor

        public PatientsController(IPatientsManager patientManager)
        {
            _patientManager = patientManager;
        }

        #endregion

        /*********************************************************************************/

        #region Create

        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(new PatientViewModel());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientViewModel patient)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    string result = await _patientManager.CreateAsync(patient);
                    if (result == StringHelpers.SuccesMessage)
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty, result);
                }

                return View(patient);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        #endregion

        /*********************************************************************************/

        #region Read

        [HttpPost]
        public string Index(string searchString)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        public async Task<IActionResult> Index(string SearchByName, string SearchByPhoneNumber, string SearchByEmail, bool includeBlackList = false, bool isDeleted = false)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _patientManager.GetListAsync(SearchByName, SearchByPhoneNumber, SearchByEmail, includeBlackList, isDeleted));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _patientManager.GetDetailsAsync(id));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        #endregion

        /*********************************************************************************/

        #region Edit

        public async Task<IActionResult> Edit(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _patientManager.GetDetailsAsync(id));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientViewModel patientViewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != patientViewModel.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var result = await _patientManager.UpdateAsync(patientViewModel);
                    if (result == StringHelpers.SuccesMessage)
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty, result);
                }

                return View(patientViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        #endregion

        /*********************************************************************************/

        #region Delete

        // POST: Pacienti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _patientManager.DeleteAsync(id);
                if (result == StringHelpers.SuccesMessage)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, result);
                    return RedirectToAction("Delete", "Patients", new { id = id });
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
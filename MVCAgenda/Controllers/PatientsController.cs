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
        private readonly IPatientsFactory _patientFactory;

        #endregion

        /*********************************************************************************/

        #region Constructor

        public PatientsController(IPatientsManager patientManager, IPatientsFactory patientFactory)
        {
            _patientManager = patientManager;
            _patientFactory = patientFactory;
        }

        #endregion

        /*********************************************************************************/

        #region Create

        public IActionResult Create()
        {
            return View(new PatientViewModel());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientViewModel patient)
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

        #endregion

        /*********************************************************************************/

        #region Read

        [HttpPost]
        public string Index(string searchString)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        public async Task<IActionResult> Index(string SearchByName, string SearchByPhoneNumber, string SearchByEmail, bool? includeBlackList = null, bool? isDeleted = null)
        {
            return View(await _patientFactory.GetListViewModelAsync(SearchByName, SearchByPhoneNumber, SearchByEmail, includeBlackList, isDeleted));

        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _patientFactory.PrepereDetailsViewModelAsync(id));
        }

        #endregion

        /*********************************************************************************/

        #region Edit

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _patientFactory.PrepereDetailsViewModelAsync(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientViewModel patientViewModel)
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

        #endregion

        /*********************************************************************************/

        #region Delete

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
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

        #endregion
    }
}
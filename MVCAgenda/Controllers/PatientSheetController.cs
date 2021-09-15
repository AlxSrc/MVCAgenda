using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Factories.PatientsSheet;
using MVCAgenda.Managers.PatientsSheets;
using MVCAgenda.Models.PatientsSheets;

namespace MVCAgenda.Controllers
{
    public class PatientSheetController : Controller
    {
        #region Fields
        private readonly IPatientsSheetsManager _patientSheetManager;
        #endregion
        /*********************************************************************************/
        #region Constructor
        public PatientSheetController(IPatientsSheetsManager patientSheetManager)
        {
            _patientSheetManager = patientSheetManager;
        }
        #endregion
        /*********************************************************************************/
        #region Details
        public async Task<IActionResult> Details(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _patientSheetManager.GetDetailsAsync(id));
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
                return View(await _patientSheetManager.GetEditDetailsAsync(id));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientSheetEditViewModel patientsheet)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != patientsheet.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var result = await _patientSheetManager.UpdateAsync(patientsheet);
                    if (result == StringHelpers.SuccesMessage)
                        return RedirectToAction("Details", "PatientSheet", new { id = patientsheet.Id });
                    else
                        ModelState.AddModelError(string.Empty, result);
                }
                return View(patientsheet);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion
    }
}

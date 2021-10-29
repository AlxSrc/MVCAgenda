using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Factories.PatientsSheet;
using MVCAgenda.Managers.PatientsSheets;
using MVCAgenda.Models.PatientSheets;

namespace MVCAgenda.Controllers
{
    [Authorize]
    public class PatientSheetsController : Controller
    {
        #region Fields

        private readonly IPatientsSheetsManager _patientSheetManager;
        private readonly IPatientsSheetsFactory _patientSheetFactory;

        #endregion

        /*********************************************************************************/

        #region Constructor

        public PatientSheetsController(IPatientsSheetsManager patientSheetManager, IPatientsSheetsFactory patientSheetFactory)
        {
            _patientSheetManager = patientSheetManager;
            _patientSheetFactory = patientSheetFactory;
        }

        #endregion

        /*********************************************************************************/

        #region Details

        public async Task<IActionResult> Details(int id)
        {
            return View(await _patientSheetFactory.PrepereDetailsViewModelAsync(id));
        }

        #endregion

        /*********************************************************************************/

        #region Edit

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _patientSheetFactory.PrepereEditViewModelAsync(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientSheetEditViewModel patientsheet)
        {
            if (id != patientsheet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _patientSheetManager.UpdateAsync(patientsheet);
                if(result == StringHelpers.SuccesMessage)
                    return RedirectToAction("Details", "PatientSheets", new { id = patientsheet.Id });
                else
                    ModelState.AddModelError(string.Empty, result);
            }

            return View(patientsheet);
        }

        #endregion
    }
}
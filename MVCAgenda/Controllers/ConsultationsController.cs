using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Factories.Consultations;
using MVCAgenda.Managers.Consultations;
using MVCAgenda.Models.Consultations;

namespace MVCAgenda.Controllers
{
    [Authorize]
    public class ConsultationsController : Controller
    {
        #region Fields

        private readonly IConsultationsManager _consultationManager;
        private readonly IConsultationsFactory _consultationFactory;

        #endregion

        /**************************************************************************************/

        #region Constructor

        public ConsultationsController(IConsultationsManager consultationManager, IConsultationsFactory consultationFactory)
        {
            _consultationManager = consultationManager;
            _consultationFactory = consultationFactory;
        }

        #endregion

        /**************************************************************************************/

        #region Create

        public async Task<IActionResult> Create(int id)
        {
            return View(await _consultationFactory.PrepereCreateViewModelAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsultationCreateViewModel consultation)
        {
            if (ModelState.IsValid)
            {
                var result = await _consultationManager.CreateAsync(consultation);
                if (result == StringHelpers.SuccesMessage)
                    return RedirectToAction("Details", "PatientSheets", new { id = consultation.PatientSheetId });
                else
                    ModelState.AddModelError(string.Empty, result);
            }

            return View(consultation);
        }

        #endregion

        /**************************************************************************************/

        #region Read

        public async Task<IActionResult> Details(int id)
        {
            return View(await _consultationFactory.PrepereDetailsViewModelAsync(id));
        }

        #endregion

        /**************************************************************************************/

        #region Edit

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _consultationFactory.PrepereEditViewModelAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ConsultationEditViewModel consultation)
        {
            if (id != consultation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _consultationManager.UpdateAsync(consultation);
                if (result == StringHelpers.SuccesMessage)
                    return RedirectToAction("Details", "PatientSheets", new { id = consultation.PatientSheetId });
                else
                    ModelState.AddModelError(string.Empty, result);
            }

            return View(consultation);
        }

        #endregion

        /**************************************************************************************/

        #region Delete

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _consultationManager.DeleteAsync(id);
            if (result == StringHelpers.SuccesMessage)
            {
                var consultation = await _consultationFactory.PrepereDetailsViewModelAsync(id);
                return RedirectToAction("Details", "PatientSheets", new { id = consultation.PatientSheetId });
            }
            else
                return RedirectToAction("Details", "Consultations", new { id = id });
        }

        #endregion
    }
}
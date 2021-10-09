using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Managers.Consultations;
using MVCAgenda.Models.Consultations;

namespace MVCAgenda.Controllers
{
    [Authorize]
    public class ConsultationsController : Controller
    {
        #region Fields
        private readonly IConsultationsManager _consultationManager;
        #endregion
        /**************************************************************************************/
        #region Constructor
        public ConsultationsController(IConsultationsManager consultationManager)
        {
            _consultationManager = consultationManager;
        }
        #endregion
        /**************************************************************************************/
        #region Create
        public IActionResult Create(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(new ConsultationCreateViewModel() { SheetPatientId = id});
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsultationCreateViewModel consultation)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var result = await _consultationManager.CreateAsync(consultation);
                    if(result == StringHelpers.SuccesMessage)
                        return RedirectToAction("Details", "PatientSheets", new { id = consultation.SheetPatientId });
                }
                return View(consultation);
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
                return View(await _consultationManager.GetDetailsAsync(id));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion
        /**************************************************************************************/
        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _consultationManager.GetEditDetailsAsync(id));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ConsultationEditViewModel consultation)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != consultation.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var result = await _consultationManager.UpdateAsync(consultation);
                    if (result == StringHelpers.SuccesMessage)
                        return RedirectToAction("Details", "PatientSheets", new { id = consultation.SheetPatientId });
                }
                return View(consultation);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion
        /**************************************************************************************/
        #region Delete

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _consultationManager.DeleteAsync(id);
                if(result == StringHelpers.SuccesMessage)
                {
                    var consultation = await _consultationManager.GetDetailsAsync(id);
                    return RedirectToAction("Details", "PatientSheets", new { id = consultation.SheetPatientId});
                }
                else
                    return RedirectToAction("Details", "Consultations", new { id = id });
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.ViewModels;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Consultations;

namespace MVCAgenda.Controllers
{
    public class ConsultationsController : Controller
    {
        #region Services
        private readonly AgendaContext _context;
        private readonly IConsultationServices _consultationServices;
        #endregion
        /**************************************************************************************/
        public ConsultationsController(AgendaContext context, IConsultationServices consultationServices)
        {
            _context = context;
            _consultationServices = consultationServices;
        }

        #region Create
        public IActionResult Create(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(new ConsultationViewModel() { SheetPatientId = id, CreationDate = DateTime.Now});
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsultationViewModel consultation)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var result = await _consultationServices.CreateConsultationAsync(consultation);
                    if(result)
                        return RedirectToAction("Details", "SheetPatient", new { id = consultation.SheetPatientId });
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
        #region Get
        public async Task<IActionResult> Details(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _consultationServices.GetConsultationAsync(id));
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
                return View(await _consultationServices.GetConsultationAsync(id));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ConsultationViewModel consultation)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != consultation.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var result = await _consultationServices.EditConsultationAsync(consultation);
                    if (result)
                        return RedirectToAction("Details", "SheetPatient", new { id = consultation.SheetPatientId });
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
        #region Hide
        #endregion
        /**************************************************************************************/
        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(_consultationServices.GetConsultationAsync(id));
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
                var result = await _consultationServices.DeleteConsultationAsync(id);
                if(result)
                    return RedirectToAction("Index", "Patients");
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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.MVCAgendaManagement;
using MVCAgenda.Core.ViewModels;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Factories;
using MVCAgenda.Service.Patients;

namespace MVCAgenda.Controllers
{
    public class PatientsController : Controller
    {
        #region Services
        private readonly AgendaContext _context;
        private readonly IPatientServices _patientServices;
        private readonly IAgendaViewsFactory _agendaViewsFactory;
        #endregion

        public PatientsController(AgendaContext context,
            IPatientServices patientServices, IAgendaViewsFactory agendaViewsFactory)
        {
            _context = context;
            _patientServices = patientServices;
            _agendaViewsFactory = agendaViewsFactory;
        }

        #region Index
        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        public async Task<IActionResult> Index(string SearchByName, string SearchByPhoneNumber, string SearchByEmail, bool includeBlackList = false, bool isDeleted = false)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _patientServices.GetPatientsAsync(SearchByName, SearchByPhoneNumber, SearchByEmail, includeBlackList, isDeleted));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion
        /*********************************************************************************/
        #region Details
        public async Task<IActionResult> Details(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _patientServices.GetPatientViewModelByIdAsync(await _patientServices.GetPatientByIdAsync(id)));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
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
                    string result = await _patientServices.CreatePatientAsync(patient);
                    if (result == "Ok")
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
        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _patientServices.GetPatientViewModelByIdAsync(await _patientServices.GetPatientByIdAsync(id)));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientViewModel patient)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != patient.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var result = await _patientServices.EditPatientAsync(patient);
                    if(result == "Succes")
                        return RedirectToAction(nameof(Index));
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
        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _patientServices.GetPatientByIdAsync(id));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Pacienti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _patientServices.HidePatientAsync(id);
                if (result == "")
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
        /*********************************************************************************/
        #region Utils
        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.Id == id);
        }
        #endregion
    }
}

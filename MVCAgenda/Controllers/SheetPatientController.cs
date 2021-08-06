using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.ViewModels;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Factories;
using MVCAgenda.Service.SheetPatients;

namespace MVCAgenda.Controllers
{
    public class SheetPatientController : Controller
    {
        #region Services
        private readonly AgendaContext _context;
        private readonly ISheetPatientServices _sheetPatientServices;
        #endregion
        public SheetPatientController(AgendaContext context, ISheetPatientServices sheetPatientServices)
        {
            _context = context;
            _sheetPatientServices = sheetPatientServices;
        }
        #region Details
        public async Task<IActionResult> Details(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _sheetPatientServices.GetSheetPatientViewModelByIdAsync(id));
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
                return View(await _sheetPatientServices.GetSheetPatientByIdAsync(id));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SheetPatientViewModel sheetPatient)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != sheetPatient.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var result = await _sheetPatientServices.EditSheetPatientAsync(sheetPatient);
                    if(result == "")
                        return RedirectToAction("Details", "Patients", new { id = sheetPatient.Id });
                }
                return View(sheetPatient);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion
        /*********************************************************************************/
        #region Utils
        private bool SheetPatientExists(int id)
        {
            return _context.SheetPatient.Any(e => e.Id == id);
        }
        #endregion
    }
}

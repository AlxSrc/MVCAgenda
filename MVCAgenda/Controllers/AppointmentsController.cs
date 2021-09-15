using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Managers.Appointments;
using MVCAgenda.Models.Appointments;
using MVCAgenda.Service.Appointments;
using MVCAgenda.Service.Medics;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.Rooms;

namespace MVCAgenda.Controllers
{
    public class AppointmentsController : Controller
    {
        #region Fields
        private readonly IAppointmentsManager _appointmentsManager;
        private readonly IPatientServices _patientServices;
        private readonly IRoomServices _roomServices;
        private readonly IMedicServices _medicServices;
        #endregion
        /**************************************************************************************/
        #region Constructor
        public AppointmentsController(
            IAppointmentsManager appointmentsManager,
            IPatientServices patientServices,
            IRoomServices roomServices,
            IMedicServices medicServices)
        {
            _appointmentsManager = appointmentsManager;
            _patientServices = patientServices;
            _roomServices = roomServices;
            _medicServices = medicServices;
        }
        #endregion
        /**************************************************************************************/
        #region Create

        public async Task<IActionResult> Create(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new AppointmentCreateViewModel();

                ViewData["RoomId"] = new SelectList(await _roomServices.GetListAsync(), "Id", "Name");
                ViewData["MedicId"] = new SelectList(await _medicServices.GetListAsync(), "Id", "Name");

                model.ResponsibleForAppointment = User.Identity.Name;
                if (id > 0)
                {
                    Patient patient = await _patientServices.GetAsync(id);
                    if (patient == null)
                        return View(model);

                    model.PatientId = id;
                    model.FirstName = patient.FirstName;
                    model.SecondName = patient.SecondName;
                    model.PhonNumber = patient.PhonNumber;
                    model.Mail = patient.Mail;
                }

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentCreateViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    string result = await _appointmentsManager.CreateAsync(model);
                    if (result == StringHelpers.SuccesMessage)
                        return RedirectToAction(nameof(Index),new { Daily = true });
                    else
                        ModelState.AddModelError(string.Empty, result);
                }

                ViewData["RoomId"] = new SelectList(await _roomServices.GetListAsync(), "Id", "Name");
                ViewData["MedicId"] = new SelectList(await _medicServices.GetListAsync(), "Id", "Name");

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        #endregion
        /**************************************************************************************/
        #region Read
        public async Task<IActionResult> Index(string SearchByName, string SearchByPhoneNumber, string SearchByEmail, string SearchByAppointmentHour, string SearchByAppointmentDate, int SearchByRoom, int SearchByMedic, string SearchByProcedure, int Id, bool Daily, bool Hidden)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["RoomId"] = new SelectList(await _roomServices.GetListAsync(), "Id", "Name");
                ViewData["MedicId"] = new SelectList(await _medicServices.GetListAsync(), "Id", "Name");

                return View(await _appointmentsManager.GetListAsync(SearchByName, SearchByPhoneNumber, SearchByEmail, SearchByAppointmentHour, SearchByAppointmentDate, SearchByRoom, SearchByMedic, SearchByProcedure, Id, Daily, Hidden));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        #endregion
        /**************************************************************************************/
        #region Update

        public async Task<IActionResult> Edit(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["RoomId"] = new SelectList(await _roomServices.GetListAsync(), "Id", "Name");
                ViewData["MedicId"] = new SelectList(await _medicServices.GetListAsync(), "Id", "Name");
                return View(await _appointmentsManager.GetEditDetailsAsync(id));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppointmentEditViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != model.Id)
                    return NotFound();

                if (ModelState.IsValid)
                {
                    string result = await _appointmentsManager.UpdateAsync(model);
                    if (result == StringHelpers.SuccesMessage)
                        return RedirectToAction(nameof(Index),new { Daily = true });
                    else
                        ModelState.AddModelError(string.Empty, result);
                }

                ViewData["RoomId"] = new SelectList(await _roomServices.GetListAsync(), "Id", "Name");
                ViewData["MedicId"] = new SelectList(await _medicServices.GetListAsync(), "Id", "Name");

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion
        /**************************************************************************************/
        #region Details
        public async Task<IActionResult> Details(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _appointmentsManager.GetDetailsAsync(id));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion
        /**************************************************************************************/
        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _appointmentsManager.GetDetailsAsync(id));
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
                var result = await _appointmentsManager.DeleteAsync(id);
                return RedirectToAction(nameof(Index), new { Daily = true });
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        #endregion
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.MVCAgendaManagement;
using MVCAgenda.Core.ViewModels;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Appointments;
using MVCAgenda.Service.Factories;

namespace MVCAgenda.Controllers
{
    public class AppointmentsController : Controller
    {
        #region Services
        private readonly AgendaContext _context;
        private readonly IAgendaViewsFactory _agendaViewsFactory;
        private readonly IAppointmentServices _appointmentServices;
        #endregion
        /**************************************************************************************/
        #region Fields
        private static DateTime ActualDateTime = DateTime.Now;

        private string DayTime = ActualDateTime.ToString("yyyy-MM-dd");
        private string _dataCreeareConsultatie = ActualDateTime.ToString("U");

        private string _responsabilProgramare = "Administrator";
        #endregion

        public AppointmentsController(AgendaContext context, IAgendaViewsFactory agendaViewsFactory, IAppointmentServices appointmentServices)
        {
            _context = context;
            _agendaViewsFactory = agendaViewsFactory;
            _appointmentServices = appointmentServices;
        }

        #region Get

        public async Task<IActionResult> Index(string SearchByName, string SearchByPhoneNumber, string SearchByEmail, string SearchByAppointmentHour, string SearchByAppointmentDate, int SearchByRoom, int SearchByMedic, string SearchByProcedure, int Id, bool Daily, bool Hidden)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["RoomId"] = new SelectList(_context.Room.Where(c => c.Hidden == false), "Id", "RoomName");
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Hidden == false), "Id", "MedicName");

                try
                {
                    return View(await _appointmentServices.GetAppointmentsAsync(SearchByName, SearchByPhoneNumber, SearchByEmail, SearchByAppointmentHour, SearchByAppointmentDate, SearchByRoom, SearchByMedic, SearchByProcedure, Id, Daily, Hidden));
                }
                catch
                {
                    return View(new MVCAgendaViewsManager());
                }

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        #endregion
        /**************************************************************************************/
        #region Create

        public async Task<IActionResult> Create(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new AppointmentViewModel();

                ViewData["RoomId"] = new SelectList(_context.Room.Where(c => c.Hidden == false), "Id", "RoomName");
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Hidden == false), "Id", "MedicName");

                model.AppointmentCreationDate = DateTime.Now.ToString();
                model.ResponsibleForAppointment = User.Identity.Name;
                model.Made = true;
                if (id > 0)
                {
                    Patient patient = await _context.Patient.FindAsync(id);
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
        public async Task<IActionResult> Create(AppointmentViewModel model)
        {if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    string result = await _appointmentServices.CreateAppointmentAsync(model);
                    if (result == "Ok")
                        return RedirectToAction(nameof(Index),new { Daily = true });
                    else
                        ModelState.AddModelError(string.Empty, result);
                }

                ViewData["RoomId"] = new SelectList(_context.Room.Where(c => c.Hidden == false), "Id", "RoomName");
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Hidden == false), "Id", "MedicName");

                return View(model);
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
                ViewData["RoomId"] = new SelectList(_context.Room.Where(c => c.Hidden == false), "Id", "RoomName");
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Hidden == false), "Id", "MedicName");
                var model = await _appointmentServices.GetAppointmentByIdAsync(id);
                return View(await _appointmentServices.GetAppointmentByIdAsync(id));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppointmentViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != model.Id)
                    return NotFound();

                if (ModelState.IsValid)
                {
                    string result = await _appointmentServices.EditAppointmentAsync(model);
                    if (result == "Ok")
                        return RedirectToAction(nameof(Index),new { Daily = true });
                    else
                        ModelState.AddModelError(string.Empty, result);
                }

                ViewData["RoomId"] = new SelectList(_context.Room.Where(c => c.Hidden == false), "Id", "RoomName");
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Hidden == false), "Id", "MedicName");

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
                return View(await _appointmentServices.GetAppointmentByIdAsync(id));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion
        /**************************************************************************************/
        #region Hide Empty, To do

        #endregion
        /**************************************************************************************/
        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _appointmentServices.GetAppointmentByIdAsync(id));
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
                var result = await _appointmentServices.HideAppointmentAsync(id);
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

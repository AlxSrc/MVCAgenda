using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.MVCAgendaManagement;
using MVCAgenda.Core.ViewModels;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Factories;

namespace MVCAgenda.Controllers
{
    public class AppointmentsController : Controller
    {
        #region Services
        private readonly AgendaContext _context;
        private readonly IAgendaViewsFactory _agendaViewsFactory;
        #endregion

        #region Fields
        private static DateTime ActualDateTime = DateTime.Now;

        private string DayTime = ActualDateTime.ToString("yyyy-MM-dd");
        private string _dataCreeareConsultatie = ActualDateTime.ToString("U");

        private string _responsabilProgramare = "Administrator";
        #endregion

        #region C-tor
        public AppointmentsController(AgendaContext context, IAgendaViewsFactory agendaViewsFactory)
        {
            _context = context;
            _agendaViewsFactory = agendaViewsFactory;
        }
        #endregion

        // GET: Programari
        public async Task<IActionResult> Index(string SearchByAppointmentHour, string SearchByAppointmentDate, int SearchByRoom, int SearchByMedic)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["RoomId"] = new SelectList(_context.Room.Where(c => c.Visible == 1), "Id", "RoomName");
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Visible == 1), "Id", "MedicName");

                var queryProgramariComplete = await (
                    from patient in _context.Patient
                    join appointment in _context.Appointment

                        .Where(p => SearchByMedic != 0 ? p.MedicId == SearchByMedic : true)
                        .Where(p => SearchByRoom != 0 ? p.RoomId == SearchByRoom : true)
                        .Where(p => !string.IsNullOrEmpty(SearchByAppointmentHour) ? p.AppointmentHour.Contains(SearchByAppointmentHour) : true)
                        .Where(p => !string.IsNullOrEmpty(SearchByAppointmentDate) ? p.AppointmentDate.Contains(SearchByAppointmentDate) : true)
                            on patient.Id equals appointment.PatientId

                    join room in _context.Room on appointment.RoomId equals room.Id
                    join medic in _context.Medic on appointment.MedicId equals medic.Id
                    orderby (appointment.AppointmentHour)
                    select _agendaViewsFactory.PrepereAppointmentViewModel(appointment, patient, medic, room)
                    ).ToListAsync();

                var model = new MVCAgendaViewsManager
                {
                    AppointmentsList = queryProgramariComplete
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }// GET: Programari
        public async Task<IActionResult> AllProgram(int id, string SearchByAppointmentHour, string SearchByAppointmentDate, int SearchByRoom, int SearchByMedic)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["RoomId"] = new SelectList(_context.Room.Where(c => c.Visible == 1), "Id", "RoomName");
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Visible == 1), "Id", "MedicName");


                var queryProgramariComplete = await (
                    from patient in _context.Patient
                    join appointment in _context.Appointment

                        .Where(p => p.PatientId == id)

                        .Where(p => SearchByMedic != 0 ? p.MedicId == SearchByMedic : true)
                        .Where(p => SearchByRoom != 0 ? p.RoomId == SearchByRoom : true)
                        .Where(p => !string.IsNullOrEmpty(SearchByAppointmentHour) ? p.AppointmentHour.Contains(SearchByAppointmentHour) : true)
                        .Where(p => !string.IsNullOrEmpty(SearchByAppointmentDate) ? p.AppointmentDate.Contains(SearchByAppointmentDate) : true)
                            on patient.Id equals appointment.PatientId

                    join room in _context.Room on appointment.RoomId equals room.Id
                    join medic in _context.Medic on appointment.MedicId equals medic.Id
                    orderby (appointment.AppointmentHour)
                    select _agendaViewsFactory.PrepereAppointmentViewModel(appointment, patient, medic, room)
                    ).ToListAsync();

                var model = new MVCAgendaViewsManager
                {
                    AppointmentsList = queryProgramariComplete
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public async Task<IActionResult> DailyIndex(string SearchByAppointmentHour, string SearchByAppointmentDate, int SearchByRoom, int SearchByMedic)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["RoomId"] = new SelectList(_context.Room.Where(c => c.Visible == 1), "RoomId", "RoomName");
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Visible == 1), "MedicId", "MedicName");

                var queryProgramariComplete = await (
                    from patient in _context.Patient
                    join appointment in _context.Appointment

                        .Where(p => p.AppointmentDate.Contains(DayTime))

                        .Where(p => SearchByMedic != 0 ? p.MedicId == SearchByMedic : true)
                        .Where(p => SearchByRoom != 0 ? p.RoomId == SearchByRoom : true)
                        .Where(p => !string.IsNullOrEmpty(SearchByAppointmentHour) ? p.AppointmentHour.Contains(SearchByAppointmentHour) : true)
                        .Where(p => !string.IsNullOrEmpty(SearchByAppointmentDate) ? p.AppointmentDate.Contains(SearchByAppointmentDate) : true)
                            on patient.Id equals appointment.PatientId

                    join room in _context.Room on appointment.RoomId equals room.Id
                    join medic in _context.Medic on appointment.MedicId equals medic.Id
                    orderby (appointment.AppointmentHour)
                    select _agendaViewsFactory.PrepereAppointmentViewModel(appointment, patient, medic, room)
                    ).ToListAsync();

                var model = new MVCAgendaViewsManager
                {
                    AppointmentsList = queryProgramariComplete
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        // GET: Programari
        public async Task<IActionResult> Deleted(string SearchByAppointmentHour, string SearchByAppointmentDate, int SearchByRoom, int SearchByMedic)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["RoomId"] = new SelectList(_context.Room.Where(c => c.Visible == 1), "RoomId", "RoomName");
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Visible == 1), "MedicId", "MedicName");

                var queryProgramariComplete = await (
                    from patient in _context.Patient
                    join appointment in _context.Appointment

                        .Where(p => p.Visible == 0)

                        .Where(p => SearchByMedic != 0 ? p.MedicId == SearchByMedic : true)
                        .Where(p => SearchByRoom != 0 ? p.RoomId == SearchByRoom : true)
                        .Where(p => !string.IsNullOrEmpty(SearchByAppointmentHour) ? p.AppointmentHour.Contains(SearchByAppointmentHour) : true)
                        .Where(p => !string.IsNullOrEmpty(SearchByAppointmentDate) ? p.AppointmentDate.Contains(SearchByAppointmentDate) : true)
                            on patient.Id equals appointment.PatientId

                    join room in _context.Room on appointment.RoomId equals room.Id
                    join medic in _context.Medic on appointment.MedicId equals medic.Id
                    orderby (appointment.AppointmentHour)
                    select _agendaViewsFactory.PrepereAppointmentViewModel(appointment, patient, medic, room)
                    ).ToListAsync();

                var model = new MVCAgendaViewsManager
                {
                    AppointmentsList = queryProgramariComplete
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }// GET: Programari
        // GET: Programari/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var appointment = await _context.Appointment
                    .Include(p => p.Room)
                    .Include(p => p.Medic)
                    .Include(p => p.Patient)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (appointment == null)
                {
                    return NotFound();
                }

                return View(appointment);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Programari/Create
        public async Task<IActionResult> Create(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new AppointmentViewModel();

                ViewData["RoomId"] = new SelectList(_context.Room.Where(c => c.Visible == 1), "RoomId", "RoomName");
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Visible == 1), "MedicId", "MedicName");

                if (id > 0)
                {
                    Patient patient = await _context.Patient.FindAsync(id);
                    if (patient == null)
                        return View(model);

                    model.Id = id;
                    model.FirstName = patient.FirstName;
                    model.SecondName = patient.SecondName;
                    model.PhonNumber = patient.PhonNumber;
                    model.Mail = patient.Mail;
                    if (patient.Blacklist == 1)
                        model.Blacklist = "<span class=\"badge bg-danger\">Da</span>";
                    else
                        model.Blacklist = "<span class=\"badge bg-success\">Nu</span>"; ;
                }

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Programari/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (await searchAppointment(model.RoomId, model.AppointmentDate, model.AppointmentHour) == true)
                {
                    string msg = $"In camera {model.RoomId}, la data {model.AppointmentDate}, ora {model.AppointmentHour} exista o consultatie";
                    ModelState.AddModelError(string.Empty, msg);
                }
                else
                {
                    _responsabilProgramare = User.Identity.Name;
                    if (model.Id > 0)
                    {
                        Patient patient = await _context.Patient.FindAsync(model.Id);
                        if (patient == null)
                        {
                            return NotFound();
                        }

                        var newAppointment = new Appointment
                        {
                            Id = model.Id,
                            PatientId = patient.Id,
                            MedicId = model.MedicId,
                            RoomId = model.RoomId,
                            AppointmentDate = model.AppointmentDate,
                            AppointmentHour = model.AppointmentHour,
                            Procedure = model.Procedure,
                            Made = model.Made ? 1 : 0,
                            ResponsibleForAppointment = _responsabilProgramare,
                            AppointmentCreationDate = _dataCreeareConsultatie,
                            Comments = model.Comments,
                            Visible = model.Visible ? 1 : 0
                        };

                        _context.Add(newAppointment);
                        await _context.SaveChangesAsync();


                        return RedirectToAction(nameof(Index));
                    }
                    else if (ModelState.IsValid)
                    {
                        var patients = await _context.Patient
                            .Where(p => p.FirstName.Contains(model.FirstName))
                            .Where(p => p.SecondName.Contains(model.SecondName))
                            .Where(p => p.PhonNumber.Contains(model.PhonNumber))
                            .Where(p => p.Mail.Contains(model.Mail))
                            .ToListAsync();

                        if (patients.Count > 1)
                            return StatusCode(500);

                        var newPatient = patients.FirstOrDefault();
                        if (newPatient == null)
                        {
                            int bl;
                            if (model.Blacklist == "Da")
                                bl = 1;
                            else
                                bl = 0;

                            newPatient = new Patient
                            {
                                FirstName = model.FirstName,
                                SecondName = model.SecondName,
                                PhonNumber = model.PhonNumber,
                                Mail = model.Mail,
                                Blacklist = bl
                            };

                            SheetPatient FisaPacientCurent = new SheetPatient();
                            _context.Add(FisaPacientCurent);
                            await _context.SaveChangesAsync();

                            int lastID = _context.SheetPatient.Count();
                            newPatient.SheetPatientId = lastID;
                            _context.Add(newPatient);
                            await _context.SaveChangesAsync();
                        }

                        var newAppointment = new Appointment
                        {
                            Id = model.Id,
                            PatientId = newPatient.Id,
                            MedicId = model.MedicId,
                            RoomId = model.RoomId,
                            AppointmentDate = model.AppointmentDate,
                            AppointmentHour = model.AppointmentHour,
                            Procedure = model.Procedure,
                            Made = model.Made ? 1 : 0,
                            ResponsibleForAppointment = _responsabilProgramare,
                            AppointmentCreationDate = _dataCreeareConsultatie,
                            Comments = model.Comments,
                            Visible = model.Visible ? 1 : 0
                        };

                        _context.Add(newAppointment);

                        await _context.SaveChangesAsync();


                        return RedirectToAction(nameof(Index));
                    }
                }


                ViewData["RoomId"] = new SelectList(_context.Room.Where(c => c.Visible == 1), "RoomId", "RoomName");
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Visible == 1), "MedicId", "MedicName");
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private async Task<bool> searchAppointment(int room, string AppointmentDate, string AppointmentHour)
        {
            bool cauta = false;

            var programari = await _context.Appointment
                        .Where(p => p.RoomId == room)
                        .Where(p => p.AppointmentDate.Equals(AppointmentDate))
                        .Where(p => p.AppointmentDate.Equals(AppointmentHour))
                        .ToListAsync();

            if (programari.Count >= 1)
                cauta = true;

            return cauta;
        }

        // GET: Programari/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var appointment = await _context.Appointment.FindAsync(id);
                if (appointment == null)
                {
                    return NotFound();
                }

                ViewData["RoomId"] = new SelectList(_context.Room.Where(c => c.Visible == 1), "RoomId", "RoomName");
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Visible == 1), "MedicId", "MedicName");
                ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Name", appointment.PatientId);
                return View(appointment);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Programari/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Appointment appointment)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != appointment.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(appointment);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AppointmentExists(appointment.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["RoomId"] = new SelectList(_context.Room.Where(c => c.Visible == 1), "RoomId", "RoomName");
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Visible == 1), "MedicId", "MedicName");
                ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "Name", appointment.PatientId);
                return View(appointment);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Programari/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var appointment = await _context.Appointment
                    .Include(p => p.Patient)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (appointment == null)
                {
                    return NotFound();
                }

                return View(appointment);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Programari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var appointment = await _context.Appointment.FindAsync(id);
                appointment.Visible = 0;
                _context.Appointment.Update(appointment);
                //_context.Programare.Remove(programare);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.Id == id);
        }

    }
}

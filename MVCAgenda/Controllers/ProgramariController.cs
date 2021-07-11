using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;

using MVCAgenda.Data;
using MVCAgenda.Domain;
using MVCAgenda.Factories;
using MVCAgenda.Models;

namespace MVCAgenda.Controllers
{
    public class ProgramariController : Controller
    {
        private readonly AgendaContext _context;


        private static DateTime ActualDateTime = DateTime.Now;

        private string DayTime = ActualDateTime.ToString("yyyy-MM-dd");
        private string _dataCreeareConsultatie = ActualDateTime.ToString("U");

        private string _responsabilProgramare = "Administrator";


        public ProgramariController(AgendaContext context)
        {
            _context = context;
        }


        // GET: Programari
        public async Task<IActionResult> Index(string SearchStringOra, string SearchStringData,int SearchCamera, int SearchMedic)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["CameraId"] = new SelectList(_context.Camera.Where(c => c.Visible == 1), "CameraId", "DenumireCamera");
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Visible == 1), "MedicId", "DenumireMedic");

                var queryProgramariComplete = await (
                    from pacient in _context.Pacient
                    join programare in _context.Programare
                        .Where(p => SearchMedic != 0 ? p.MedicId == SearchMedic : true)
                        .Where(p => SearchCamera != 0 ? p.CameraId == SearchCamera : true)
                        .Where(p => !string.IsNullOrEmpty(SearchStringOra) ? p.OraConsultatie.Contains(SearchStringOra) : true)
                        .Where(p => !string.IsNullOrEmpty(SearchStringData) ? p.DataConsultatie.Contains(SearchStringData) : true)
                            on pacient.PacientId equals programare.PacientId
                    join camera in _context.Camera on programare.CameraId equals camera.CameraId
                    join medic in _context.Medic on programare.MedicId equals medic.MedicId
                    orderby (programare.DataConsultatie)
                    select AgendaFactory.PrepareAfisareProgramareViewModel(programare, pacient, camera, medic)
                    ).ToListAsync();

                var model = new ListeViewModel
                {
                    //Programari = programariModel

                    ProgramariComplete = queryProgramariComplete
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }// GET: Programari
        public async Task<IActionResult> AllProgram(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var queryProgramariComplete = await (
                    from pacient in _context.Pacient
                    join programare in _context.Programare
                            on pacient.PacientId equals programare.PacientId
                    join camera in _context.Camera
                            on programare.CameraId equals camera.CameraId
                    join medic in _context.Medic
                            on programare.MedicId equals medic.MedicId
                    orderby (programare.DataConsultatie)
                    select AgendaFactory.PrepareAfisareProgramareViewModel(programare, pacient, camera, medic)
                    ).ToListAsync();

                var model = new ListeViewModel
                {
                    //Programari = programariModel

                    ProgramariComplete = queryProgramariComplete
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public async Task<IActionResult> DailyIndex(string SearchStringOra, string SearchStringData, string SearchStringMedic)
        {
            if (User.Identity.IsAuthenticated)
            {
                var queryProgramariComplete = await (
                    from pacient in _context.Pacient
                    join programare in _context.Programare
                        .Where(p => p.DataConsultatie.Contains(DayTime))
                        .Where(p => !string.IsNullOrEmpty(SearchStringOra) ? p.OraConsultatie.Contains(SearchStringOra) : true)
                        .Where(p => !string.IsNullOrEmpty(SearchStringData) ? p.DataConsultatie.Contains(SearchStringData) : true)
                            on pacient.PacientId equals programare.PacientId
                    join camera in _context.Camera on programare.CameraId equals camera.CameraId
                    join medic in _context.Medic on programare.MedicId equals medic.MedicId
                    orderby (programare.DataConsultatie)
                    select AgendaFactory.PrepareAfisareProgramareViewModel(programare, pacient, camera, medic)
                    ).ToListAsync();

                var model = new ListeViewModel
                {
                    //Programari = programariModel

                    ProgramariComplete = queryProgramariComplete
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        // GET: Programari
        public async Task<IActionResult> Deleted(string SearchStringOra, string SearchStringData, string SearchStringMedic)
        {
            if (User.Identity.IsAuthenticated)
            {
                var queryProgramariComplete = await (
                    from pacient in _context.Pacient
                    join programare in _context.Programare
                        .Where(p => p.Visible == 0)
                        .Where(p => !string.IsNullOrEmpty(SearchStringOra) ? p.OraConsultatie.Contains(SearchStringOra) : true)
                        .Where(p => !string.IsNullOrEmpty(SearchStringData) ? p.DataConsultatie.Contains(SearchStringData) : true)
                            on pacient.PacientId equals programare.PacientId
                    join camera in _context.Camera on programare.CameraId equals camera.CameraId
                    join medic in _context.Medic on programare.MedicId equals medic.MedicId
                    orderby (programare.DataConsultatie)
                    select AgendaFactory.PrepareAfisareProgramareViewModel(programare, pacient, camera, medic)
                    ).ToListAsync();

                var model = new ListeViewModel
                {
                    //Programari = programariModel

                    ProgramariComplete = queryProgramariComplete
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

                var programare = await _context.Programare
                    .Include(p => p.Camera)
                    .Include(p => p.Medic)
                    .Include(p => p.Persoana)
                    .FirstOrDefaultAsync(m => m.ProgramareId == id);
                if (programare == null)
                {
                    return NotFound();
                }

                return View(programare);
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
                var model = new ProgramareCompletaViewModel();

                ViewData["CameraId"] = new SelectList(_context.Camera.Where(c => c.Visible == 1), "CameraId", "DenumireCamera");
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Visible == 1), "MedicId", "DenumireMedic");

                if (id > 0)
                {
                    Pacient pacient = await _context.Pacient.FindAsync(id);
                    if (pacient == null)
                        return View(model);

                    model.PacientId = id;
                    model.Nume = pacient.Nume;
                    model.Prenume = pacient.Prenume;
                    model.NrDeTelefon = pacient.NrDeTelefon;
                    model.Mail = pacient.Mail;
                    if (pacient.Blacklist == 1)
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
        public async Task<IActionResult> Create(ProgramareCompletaViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (await cautaProgramare(model.CameraId, model.DataConsultatie, model.OraConsultatie) == true)
                {
                    string msg = $"In camera {model.CameraId}, la data {model.DataConsultatie}, ora {model.OraConsultatie} exista o consultatie";
                    ModelState.AddModelError(string.Empty, msg);
                }
                else
                {
                    _responsabilProgramare = User.Identity.Name;
                    if (model.PacientId > 0)
                    {
                        Pacient pacient = await _context.Pacient.FindAsync(model.PacientId);
                        if (pacient == null)
                        {
                            return NotFound();
                        }

                        var programareNoua = new Programare
                        {
                            ProgramareId = model.ProgramareId,
                            PacientId = pacient.PacientId,
                            //PacientId = programare.PacientId,
                            MedicId = model.MedicId,
                            CameraId = model.CameraId,
                            DataConsultatie = model.DataConsultatie,
                            OraConsultatie = model.OraConsultatie,
                            Procedura = model.Procedura,
                            Efectuata = model.Efectuata,
                            ResponsabilProgramare = _responsabilProgramare,
                            DataCreeareConsultatie = _dataCreeareConsultatie,
                            Visible = model.Visible ? 1 : 0
                        };

                        _context.Add(programareNoua);
                        await _context.SaveChangesAsync();


                        return RedirectToAction(nameof(Index));
                    }
                    else if (ModelState.IsValid)
                    {
                        var persoane = await _context.Pacient
                            .Where(p => p.Nume.Contains(model.Nume))
                            .Where(p => p.Prenume.Contains(model.Prenume))
                            .Where(p => p.NrDeTelefon.Contains(model.NrDeTelefon))
                            .Where(p => p.Mail.Contains(model.Mail))
                            .ToListAsync();

                        if (persoane.Count > 1)
                            return StatusCode(500);

                        var persoanaNoua = persoane.FirstOrDefault();
                        if (persoanaNoua == null)
                        {
                            int bl;
                            if (model.Blacklist == "Da")
                                bl = 1;
                            else
                                bl = 0;

                            persoanaNoua = new Pacient
                            {
                                Nume = model.Nume,
                                Prenume = model.Prenume,
                                NrDeTelefon = model.NrDeTelefon,
                                Mail = model.Mail,
                                Blacklist = bl
                            };

                            FisaPacient FisaPacientCurent = new FisaPacient();
                            _context.Add(FisaPacientCurent);
                            await _context.SaveChangesAsync();

                            int lastID = _context.FisaPacient.Count();
                            persoanaNoua.FisaPacientId = lastID;
                            _context.Add(persoanaNoua);
                            await _context.SaveChangesAsync();
                        }

                        var programareNoua = new Programare
                        {
                            ProgramareId = model.ProgramareId,
                            PacientId = persoanaNoua.PacientId,
                            //PacientId = programare.PacientId,
                            MedicId = model.MedicId,
                            CameraId = model.CameraId,
                            DataConsultatie = model.DataConsultatie,
                            OraConsultatie = model.OraConsultatie,
                            Procedura = model.Procedura,
                            Efectuata = model.Efectuata,
                            ResponsabilProgramare = _responsabilProgramare,
                            DataCreeareConsultatie = _dataCreeareConsultatie,
                            Visible = model.Visible ? 1 : 0
                        };

                        _context.Add(programareNoua);

                        await _context.SaveChangesAsync();


                        return RedirectToAction(nameof(Index));
                    }
                }

                ViewData["CameraId"] = new SelectList(_context.Camera.Where(c => c.Visible == 1), "CameraId", "DenumireCamera", model.CameraId);
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Visible == 1), "MedicId", "DenumireMedic", model.MedicId);
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private async Task<bool> cautaProgramare(int camera, string dataConsultatie, string oraConsultatie)
        {
            bool cauta = false;

            var programari = await _context.Programare
                        .Where(p => p.CameraId == camera)
                        .Where(p => p.DataConsultatie.Equals(dataConsultatie))
                        .Where(p => p.OraConsultatie.Equals(oraConsultatie))
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

                var programare = await _context.Programare.FindAsync(id);
                if (programare == null)
                {
                    return NotFound();
                }
                ViewData["CameraId"] = new SelectList(_context.Camera.Where(c => c.Visible == 1), "CameraId", "DenumireCamera", programare.CameraId);
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Visible == 1), "MedicId", "DenumireMedic", programare.MedicId);
                ViewData["PacientId"] = new SelectList(_context.Pacient, "PacientId", "Nume", programare.PacientId);
                return View(programare);
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
        public async Task<IActionResult> Edit(int id, Programare programare)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != programare.ProgramareId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(programare);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProgramareExists(programare.ProgramareId))
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
                ViewData["CameraId"] = new SelectList(_context.Camera.Where(c => c.Visible == 1), "CameraId", "DenumireCamera", programare.CameraId);
                ViewData["MedicId"] = new SelectList(_context.Medic.Where(m => m.Visible == 1), "MedicId", "DenumireMedic", programare.MedicId);
                ViewData["PacientId"] = new SelectList(_context.Pacient, "PacientId", "NrDeTelefon", programare.PacientId);
                return View(programare);
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

                var programare = await _context.Programare
                    .Include(p => p.Persoana)
                    .FirstOrDefaultAsync(m => m.ProgramareId == id);
                if (programare == null)
                {
                    return NotFound();
                }

                return View(programare);
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
                var programare = await _context.Programare.FindAsync(id);
                programare.Visible = 0;
                _context.Programare.Update(programare);
                //_context.Programare.Remove(programare);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private bool ProgramareExists(int id)
        {
            return _context.Programare.Any(e => e.ProgramareId == id);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using MVCAgenda.Data;
using MVCAgenda.Domain;
using MVCAgenda.Factories;
using MVCAgenda.Models;

namespace MVCAgenda.Controllers
{
    public class PacientiController : Controller
    {
        private readonly AgendaContext _context;

        public PacientiController(AgendaContext context)
        {
            _context = context;
        }

        // GET: Pacienti
        public async Task<IActionResult> Index(string SearchStringNume, string SearchStringNumarDeTelefon, string SearchStringEmail, bool includeBlackList = true, bool isDeleted = false)
        {
            if (User.Identity.IsAuthenticated)
            {
                IQueryable<Pacient> query = _context.Pacient; // aici incepe syntaxa SQL (da' in c#)

                if (isDeleted)
                    query = query.Where(p => p.Visible == 0); // filtram date daca e
                else
                    query = query.Where(p => p.Visible == 1);

                if (!includeBlackList)
                    query = query.Where(p => p.Blacklist == 1);

                if (!string.IsNullOrEmpty(SearchStringNume))
                    query = query.Where(p => p.Nume.Contains(SearchStringNume));

                if (!string.IsNullOrEmpty(SearchStringNumarDeTelefon))
                    query = query.Where(p => p.NrDeTelefon.Contains(SearchStringNumarDeTelefon));

                if (!string.IsNullOrEmpty(SearchStringEmail))
                    query = query.Where(p => p.Mail.Contains(SearchStringEmail));

                var pacienti = await query.ToListAsync(); // aici aducem datele despre pacienti prin sintaxa SQL

                var pacientiModel = pacienti // lista de pacient adusa
                    .Select(p => AgendaFactory.PreparePacientViewModel(p)) // aplicam metoda PreparePersoanaViewModel pentru fiecare pacient
                    .ToList(); // transformam colectia in lista

                var model = new ListeViewModel
                {
                    Pacienti = pacientiModel
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }
        
        public async Task<IActionResult> Blacklist(string SearchStringNume, string SearchStringNumarDeTelefon, string SearchStringEmail)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await Index(SearchStringNume, SearchStringNumarDeTelefon, SearchStringEmail, includeBlackList: false);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public string Blacklist(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }
        public async Task<IActionResult> Deleted(string SearchStringNume, string SearchStringNumarDeTelefon, string SearchStringEmail)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await Index(SearchStringNume, SearchStringNumarDeTelefon, SearchStringEmail, isDeleted: true);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public string Deleted(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }


        // GET: Pacienti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var persoana = await _context.Pacient.FirstOrDefaultAsync(m => m.PacientId == id);
                if (persoana == null)
                {
                    return NotFound();
                }

                var persoanaViewModel = AgendaFactory.PreparePacientViewModel(persoana);

                return View(persoanaViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Pacienti/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(new Pacient());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Pacienti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pacient pacient)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var persoane = await _context.Pacient
                            .Where(p => p.Nume.Contains(pacient.Nume))
                            .Where(p => p.Prenume.Contains(pacient.Prenume))
                            .Where(p => p.NrDeTelefon.Contains(pacient.NrDeTelefon))
                            .ToListAsync();

                    if (persoane.Count >= 1)
                    {
                        string msg = $"Exista un pacient cu numele: {pacient.Nume}, prenumele: {pacient.Prenume} si numarul de telefon: {pacient.NrDeTelefon}";
                        ModelState.AddModelError(string.Empty, msg);
                    }
                    else
                    {
                        FisaPacient FisaPacientCurent = new FisaPacient();
                        _context.Add(FisaPacientCurent);
                        await _context.SaveChangesAsync();

                        int lastID = _context.FisaPacient.Count();
                        pacient.FisaPacientId = lastID;
                        _context.Add(pacient);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(pacient);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        // GET: Pacienti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var persoana = await _context.Pacient.FindAsync(id);
                if (persoana == null)
                {
                    return NotFound();
                }
                return View(persoana);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Pacienti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Pacient persoana)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != persoana.PacientId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(persoana);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PersoanaExists(persoana.PacientId))
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
                return View(persoana);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Pacienti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var persoana = await _context.Pacient
                    .FirstOrDefaultAsync(m => m.PacientId == id);
                if (persoana == null)
                {
                    return NotFound();
                }

                return View(persoana);
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
                var persoana = await _context.Pacient.FindAsync(id);
                persoana.Visible = 0;
                _context.Pacient.Update(persoana);
                //_context.Pacient.Remove(pacient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private bool PersoanaExists(int id)
        {
            return _context.Pacient.Any(e => e.PacientId == id);
        }
    }
}

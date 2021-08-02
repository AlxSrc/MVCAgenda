using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Factories;

namespace MVCAgenda.Controllers
{
    public class SheetPatientController : Controller
    {
        private readonly AgendaContext _context;
        private readonly IAgendaViewsFactory _agendaViewsFactory;

        public SheetPatientController(AgendaContext context, IAgendaViewsFactory agendaViewsFactory)
        {
            _context = context;
            _agendaViewsFactory = agendaViewsFactory;
        }

        // GET: FisePacienti
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.SheetPatient.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: FisePacienti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var sheetPatient = await _context.SheetPatient
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (sheetPatient == null)
                {
                    return NotFound();
                }

                IQueryable<Consultation> query = _context.Consultation;
                query = query.Where(p => p.Id == id).OrderBy(p => p.CreationDate);


                var consultations = await query.OrderByDescending(c => c.CreationDate).ToListAsync();

                var sheetPatientViewModel = _agendaViewsFactory.PrepereSheetPatientViewModel(sheetPatient, consultations);
                var ased = sheetPatientViewModel;

                return View(sheetPatientViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: FisePacienti/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: FisePacienti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SheetPatient sheetPatient)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(sheetPatient);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Pacienti");
                }
                return View(sheetPatient);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: FisePacienti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var fisaPacient = await _context.SheetPatient.FindAsync(id);
                if (fisaPacient == null)
                {
                    return NotFound();
                }
                return View(fisaPacient);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: FisePacienti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,SheetPatient sheetPatient)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != sheetPatient.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(sheetPatient);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!FisaPacientExists(sheetPatient.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Details", "Pacienti", new { id = sheetPatient.Id });
                }
                return View(sheetPatient);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: FisePacienti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var fisaPacient = await _context.SheetPatient
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (fisaPacient == null)
                {
                    return NotFound();
                }

                return View(fisaPacient);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: FisePacienti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var fisaPacient = await _context.SheetPatient.FindAsync(id);
                _context.SheetPatient.Remove(fisaPacient);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Pacienti");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private bool FisaPacientExists(int id)
        {
            return _context.SheetPatient.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Data.DataBaseManager;

namespace MVCAgenda.Controllers
{
    public class ConsultationsController : Controller
    {
        private readonly AgendaContext _context;

        private static DateTime ActualDateTime = DateTime.Now;

        private string DayTime = ActualDateTime.ToString("yyyy-MM-dd");
        private string _dataCreeareConsultatie = ActualDateTime.ToString("U");

        public ConsultationsController(AgendaContext context)
        {
            _context = context;
        }

        // GET: Consultatii
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Consultation.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Consultatii/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var consultation = await _context.Consultation
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (consultation == null)
                {
                    return NotFound();
                }

                return View(consultation);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Consultatii/Create
        public IActionResult Create(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new Consultation();
                model.SheetPatientId = id;
                model.CreationDate = DateTime.Parse(_dataCreeareConsultatie);
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Consultatii/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Consultation consultation)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(consultation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "FisePacienti", new { id = consultation.SheetPatientId });
                }
                return View(consultation);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Consultatii/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var consultation = await _context.Consultation.FindAsync(id);
                if (consultation == null)
                {
                    return NotFound();
                }
                return View(consultation);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Consultatii/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Consultation consultation)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != consultation.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(consultation);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ConsultationExists(consultation.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return RedirectToAction("Details", "Pacienti", new { id = consultation.SheetPatientId });
                }
                return View(consultation);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Consultatii/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var consultation = await _context.Consultation
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (consultation == null)
                {
                    return NotFound();
                }

                return View(consultation);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Consultatii/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var consultation = await _context.Consultation.FindAsync(id);
                _context.Consultation.Remove(consultation);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Pacienti");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private bool ConsultationExists(int id)
        {
            return _context.Consultation.Any(e => e.Id == id);
        }
    }
}

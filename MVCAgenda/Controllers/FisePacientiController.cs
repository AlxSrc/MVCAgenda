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

namespace MVCAgenda.Controllers
{
    public class FisePacientiController : Controller
    {
        private readonly AgendaContext _context;

        public FisePacientiController(AgendaContext context)
        {
            _context = context;
        }

        // GET: FisePacienti
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.FisaPacient.ToListAsync());
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

                var fisaPacient = await _context.FisaPacient
                    .FirstOrDefaultAsync(m => m.FisaPacientId == id);
                if (fisaPacient == null)
                {
                    return NotFound();
                }

                IQueryable<Consultatie> query = _context.Consultatie;
                query = query.Where(p => p.FisaPacientId == id).OrderBy(p => p.DataCreeare);


                var consultatii = await query.OrderByDescending(c => c.DataCreeare).ToListAsync();

                var fisaPacientViewModel = AgendaFactory.PrepareFisaPacientViewModel(fisaPacient, consultatii);
                var ased = fisaPacientViewModel;

                return View(fisaPacientViewModel);
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
        public async Task<IActionResult> Create([Bind("FisaPacientId,AntecedenteH,AntecedenteP,CNP,Sexul,Localitatea,Strada,DataNasterii")] FisaPacient fisaPacient)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(fisaPacient);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Pacienti");
                }
                return View(fisaPacient);
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

                var fisaPacient = await _context.FisaPacient.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("FisaPacientId,AntecedenteH,AntecedenteP,ExamenFizic,CNP,Sexul,Localitatea,Strada,DataNasterii")] FisaPacient fisaPacient)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != fisaPacient.FisaPacientId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(fisaPacient);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!FisaPacientExists(fisaPacient.FisaPacientId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Details", "Pacienti", new { id = fisaPacient.FisaPacientId });
                }
                return View(fisaPacient);
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

                var fisaPacient = await _context.FisaPacient
                    .FirstOrDefaultAsync(m => m.FisaPacientId == id);
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
                var fisaPacient = await _context.FisaPacient.FindAsync(id);
                _context.FisaPacient.Remove(fisaPacient);
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
            return _context.FisaPacient.Any(e => e.FisaPacientId == id);
        }
    }
}

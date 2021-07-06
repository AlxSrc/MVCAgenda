using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Data;
using MVCAgenda.Domain;

namespace MVCAgenda.Controllers
{
    public class MediciController : Controller
    {
        private readonly AgendaContext _context;

        public MediciController(AgendaContext context)
        {
            _context = context;
        }

        // GET: Medici
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Medic.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Medici/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var medic = await _context.Medic
                    .FirstOrDefaultAsync(m => m.MedicId == id);
                if (medic == null)
                {
                    return NotFound();
                }

                return View(medic);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Medici/Create
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

        // POST: Medici/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicId,DenumireMedic")] Medic medic)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(medic);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Manage", "Home");
                }
                return View(medic);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Medici/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var medic = await _context.Medic.FindAsync(id);
                if (medic == null)
                {
                    return NotFound();
                }
                return View(medic);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Medici/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicId,DenumireMedic")] Medic medic)
        {
            if (User.Identity.IsAuthenticated)
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(medic);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MedicExists(medic.MedicId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Manage", "Home");
                }
                return View(medic);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            if (id != medic.MedicId)
            {
                return NotFound();
            }
        }

        // GET: Medici/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var medic = await _context.Medic
                    .FirstOrDefaultAsync(m => m.MedicId == id);
                if (medic == null)
                {
                    return NotFound();
                }

                return View(medic);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Medici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var medic = await _context.Medic.FindAsync(id);
                medic.Visible = 0;
                _context.Medic.Update(medic);
                await _context.SaveChangesAsync();
                return RedirectToAction("Manage", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private bool MedicExists(int id)
        {
            return _context.Medic.Any(e => e.MedicId == id);
        }
    }
}

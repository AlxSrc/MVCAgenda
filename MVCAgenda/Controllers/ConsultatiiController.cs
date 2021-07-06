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
    public class ConsultatiiController : Controller
    {
        private readonly AgendaContext _context;

        private static DateTime ActualDateTime = DateTime.Now;

        private string DayTime = ActualDateTime.ToString("yyyy-MM-dd");
        private string _dataCreeareConsultatie = ActualDateTime.ToString("U");

        public ConsultatiiController(AgendaContext context)
        {
            _context = context;
        }

        // GET: Consultatii
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Consultatie.ToListAsync());
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

                var consultatie = await _context.Consultatie
                    .FirstOrDefaultAsync(m => m.ConsultatieId == id);
                if (consultatie == null)
                {
                    return NotFound();
                }

                return View(consultatie);
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
                var model = new Consultatie();
                model.FisaPacientId = id;
                model.DataCreeare = DateTime.Parse(_dataCreeareConsultatie);
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
        public async Task<IActionResult> Create([Bind("ConsultatieId,FisaPacientId,DataCreeare,Simptome,Diagnostic,Prescriptii")] Consultatie consultatie)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(consultatie);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "FisePacienti", new { id = consultatie.FisaPacientId });
                }
                return View(consultatie);
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

                var consultatie = await _context.Consultatie.FindAsync(id);
                if (consultatie == null)
                {
                    return NotFound();
                }
                return View(consultatie);
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
        public async Task<IActionResult> Edit(int id, [Bind("ConsultatieId,FisaPacientId,DataCreeare,Simptome,Diagnostic,Prescriptii")] Consultatie consultatie)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != consultatie.ConsultatieId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(consultatie);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ConsultatieExists(consultatie.ConsultatieId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return RedirectToAction("Details", "Pacienti", new { id = consultatie.FisaPacientId });
                }
                return View(consultatie);
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

                var consultatie = await _context.Consultatie
                    .FirstOrDefaultAsync(m => m.ConsultatieId == id);
                if (consultatie == null)
                {
                    return NotFound();
                }

                return View(consultatie);
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
                var consultatie = await _context.Consultatie.FindAsync(id);
                _context.Consultatie.Remove(consultatie);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Pacienti");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private bool ConsultatieExists(int id)
        {
            return _context.Consultatie.Any(e => e.ConsultatieId == id);
        }
    }
}

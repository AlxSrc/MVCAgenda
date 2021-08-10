using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.ViewModels;
using MVCAgenda.Data.DataBaseManager;

namespace MVCAgenda.Controllers
{
    public class MedicsController : Controller
    {
        private readonly AgendaContext _context;

        public MedicsController(AgendaContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var medic = await _context.Medic
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (medic == null)
                {
                    return NotFound();
                }

                MedicViewModel Medic = new MedicViewModel() { Id = medic.Id, MedicName = medic.MedicName, Hidden = medic.Hidden };
                return View(Medic);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicViewModel medic)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(new Medic() { MedicName = medic.MedicName, Hidden = false});
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var medic = await _context.Medic
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (medic == null)
                {
                    return NotFound();
                }

                MedicViewModel Medic = new MedicViewModel() { Id = medic.Id, MedicName = medic.MedicName, Hidden = medic.Hidden };
                return View(Medic);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Medic medic)
        {
            if (User.Identity.IsAuthenticated)
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(new Medic() {Id = medic.Id, MedicName = medic.MedicName, Hidden = medic.Hidden});
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MedicExists(medic.Id))
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
            if (id != medic.Id)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var medic = await _context.Medic
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (medic == null)
                {
                    return NotFound();
                }

                MedicViewModel Medic = new MedicViewModel() { Id = medic.Id, MedicName = medic.MedicName, Hidden = medic.Hidden };
                return View(Medic);
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
                var medic = await _context.Medic.FindAsync(id);
                medic.Hidden = true;
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
            return _context.Medic.Any(e => e.Id == id);
        }
    }
}

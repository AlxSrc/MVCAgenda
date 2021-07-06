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
    public class CamereController : Controller
    {
        private readonly AgendaContext _context;

        public CamereController(AgendaContext context)
        {
            _context = context;
        }

        // GET: Camere
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Camera.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Camere/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var camera = await _context.Camera
                    .FirstOrDefaultAsync(m => m.CameraId == id);
                if (camera == null)
                {
                    return NotFound();
                }

                return View(camera);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Camere/Create
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

        // POST: Camere/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CameraId,DenumireCamera")] Camera camera)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(camera);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Manage", "Home");
                }
                return View(camera);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Camere/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var camera = await _context.Camera.FindAsync(id);
                if (camera == null)
                {
                    return NotFound();
                }
                return View(camera);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Camere/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CameraId,DenumireCamera")] Camera camera)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != camera.CameraId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(camera);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CameraExists(camera.CameraId))
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
                return View(camera);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Camere/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var camera = await _context.Camera
                    .FirstOrDefaultAsync(m => m.CameraId == id);
                if (camera == null)
                {
                    return NotFound();
                }

                return View(camera);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Camere/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var camera = await _context.Camera.FindAsync(id);
                camera.Visible = 0;
                _context.Camera.Update(camera);
                await _context.SaveChangesAsync();
                return RedirectToAction("Manage", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private bool CameraExists(int id)
        {
            return _context.Camera.Any(e => e.CameraId == id);
        }
    }
}

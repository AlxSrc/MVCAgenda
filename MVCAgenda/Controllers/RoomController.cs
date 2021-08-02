using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Data.DataBaseManager;

namespace MVCAgenda.Controllers
{
    public class RoomController : Controller
    {
        private readonly AgendaContext _context;

        public RoomController(AgendaContext context)
        {
            _context = context;
        }

        // GET: Camere
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Room.ToListAsync());
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

                var camera = await _context.Room
                    .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("CameraId,DenumireCamera")] Room room)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(room);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Manage", "Home");
                }
                return View(room);
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

                var room = await _context.Room.FindAsync(id);
                if (room == null)
                {
                    return NotFound();
                }
                return View(room);
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
        public async Task<IActionResult> Edit(int id, [Bind("CameraId,DenumireCamera")] Room room)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != room.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(room);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!RoomExists(room.Id))
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
                return View(room);
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

                var camera = await _context.Room
                    .FirstOrDefaultAsync(m => m.Id == id);
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
                var camera = await _context.Room.FindAsync(id);
                camera.Visible = 0;
                _context.Room.Update(camera);
                await _context.SaveChangesAsync();
                return RedirectToAction("Manage", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private bool RoomExists(int id)
        {
            return _context.Room.Any(e => e.Id == id);
        }
    }
}

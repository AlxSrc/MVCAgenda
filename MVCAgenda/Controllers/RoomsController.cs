using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.ViewModels;
using MVCAgenda.Data.DataBaseManager;

namespace MVCAgenda.Controllers
{
    public class RoomsController : Controller
    {
        private readonly AgendaContext _context;

        public RoomsController(AgendaContext context)
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

                var room = await _context.Room
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (room == null)
                {
                    return NotFound();
                }

                RoomViewModel Room = new RoomViewModel() { Id = room.Id, RoomName = room.RoomName, Hidden = room.Hidden };
                return View(Room);
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
        public async Task<IActionResult> Create(RoomViewModel room)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(new Room() {RoomName = room.RoomName, Hidden = false});
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

                RoomViewModel Room = new RoomViewModel() { Id = room.Id, RoomName = room.RoomName, Hidden = room.Hidden };
                return View(Room);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,RoomViewModel room)
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
                        Room Room = new Room() { Id = room.Id, RoomName = room.RoomName, Hidden = room.Hidden };
                        _context.Update(Room);
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var room = await _context.Room
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (room == null)
                {
                    return NotFound();
                }

                RoomViewModel Room = new RoomViewModel() { Id = room.Id, RoomName = room.RoomName, Hidden = room.Hidden };
                return View(Room);
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
                var camera = await _context.Room.FindAsync(id);
                camera.Hidden = true;
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

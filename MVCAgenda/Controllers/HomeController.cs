using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCAgenda.Core.AccountModels;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.MVCAgendaManagement;
using MVCAgenda.Data.DataBaseManager;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AgendaContext _context;

        public HomeController(ILogger<HomeController> logger, AgendaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Manage()
        {
            if (User.Identity.IsAuthenticated)
            {
                IQueryable<Medic> queryMedici = _context.Medic.Where(m => m.Visible == 1);
                var medics = await queryMedici.ToListAsync();

                IQueryable<Room> queryCamere = _context.Room.Where(c => c.Visible == 1);
                var rooms = await queryCamere.ToListAsync();

                var model = new ManageViewModel
                {
                    Medics = medics,
                    Rooms = rooms
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCAgenda.Data;
using MVCAgenda.Domain;
using MVCAgenda.Models;
using System;
using System.Collections.Generic;
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
                var medici = await queryMedici.ToListAsync();

                IQueryable<Camera> queryCamere = _context.Camera.Where(c => c.Visible == 1);
                var camere = await queryCamere.ToListAsync();

                var model = new ManageViewModel
                {
                    Medici = medici,
                    Camere = camere
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

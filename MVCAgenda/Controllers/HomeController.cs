using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Factories.Home;
using MVCAgenda.Models.Accounts;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MVCAgenda.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Fields

        private readonly IHomeFactory _homeFactory;

        #endregion

        /**************************************************************************************/

        #region Constructor

        public HomeController(IHomeFactory homeFactory)
        {
            _homeFactory = homeFactory;
        }

        #endregion

        /**************************************************************************************/

        #region Index

        public async Task<IActionResult> Index()
        { 
            return View(await _homeFactory.PrepereHomeViewModel(User.Identity.Name));
        }

        #endregion

        /**************************************************************************************/

        #region Informations

        public async Task<IActionResult> Informations()
        {
            return View();
        }

        #endregion

        /**************************************************************************************/

        #region Error

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}
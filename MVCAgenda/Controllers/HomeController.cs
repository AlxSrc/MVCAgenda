﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Managers.Medics;
using MVCAgenda.Managers.Rooms;
using MVCAgenda.Models.Accounts;
using MVCAgenda.Models.Home;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MVCAgenda.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Fields

        private readonly IRoomsManager _roomsManager;
        private readonly IMedicsManager _medicsManager;

        #endregion

        /**************************************************************************************/

        #region Constructor

        public HomeController(IRoomsManager roomsManager, IMedicsManager medicsManager)
        {
            _roomsManager = roomsManager;
            _medicsManager = medicsManager;
        }

        #endregion

        /**************************************************************************************/

        #region Index

        public IActionResult Index()
        {
            return View();
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
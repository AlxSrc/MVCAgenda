using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Managers.ManageAccount;
using MVCAgenda.Models.Accounts.ManageAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Controllers
{
    public class ManageAccountController : Controller
    {
        #region Fields
        private readonly IManageAccountManager _manageAccountManager;
        #endregion
        /**************************************************************************************/
        #region Constructor
        public ManageAccountController(IManageAccountManager manageAccountManager)
        {
            _manageAccountManager = manageAccountManager;
        }
        #endregion
        /**************************************************************************************/
        #region Profile

        public async Task<IActionResult> Edit()
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = User.Identity.Name;
                return View(_manageAccountManager.GetUserProfileAsync(model));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    
                }

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion
        /**************************************************************************************/
        #region E-mail
        #endregion
        /**************************************************************************************/
        #region Password
        #endregion
        /**************************************************************************************/
        #region Two-factor autentification
        #endregion
        /**************************************************************************************/
        #region Personal data
        #endregion
    }
}

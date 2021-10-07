using System;
using System.Net.Mail;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Core.Users;
using MVCAgenda.Models.Accounts;

namespace MVCAgenda.Controllers
{
    [Authorize]
    public class AccountController : Controller 
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        #endregion
        /**************************************************************************************/
        #region Constructor
        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion
        /**************************************************************************************/
        #region Login

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    //adding user to cache
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }
        #endregion
        /**************************************************************************************/
        #region Register
        [Authorize(Roles = "Administrator")]
        public IActionResult Register()
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    MailAddress address = new MailAddress(model.Email);
                    string userName = address.User;
                    var user = new ApplicationUser
                    {
                        UserName = userName,
                        Email = model.Email
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        await _userManager.AddToRoleAsync(user, Roles.Nurse.ToString());
                        return RedirectToAction("index", "Home");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        #endregion
        /**************************************************************************************/
        #region Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
        #endregion
    }
}

using System;
using System.Net.Mail;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Core.Users;
using MVCAgenda.Managers.Medics;
using MVCAgenda.Models.Accounts;
using MVCAgenda.Models.Medics;

namespace MVCAgenda.Controllers
{
    [Authorize]
    public class AccountController : Controller 
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMedicsManager _medicsManager;
        #endregion
        /**************************************************************************************/
        #region Constructor
        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IMedicsManager medicsManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _medicsManager = medicsManager;
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
            var test = false;
            if(test)
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                MailAddress emailAddress = new MailAddress("moderator_agenda@gmail.com");
                string userName = emailAddress.User;
                var userModerator = new ApplicationUser
                {
                    UserName = userName,
                    Email = emailAddress.Address
                };

                var result = await _userManager.CreateAsync(userModerator, "{Al@ka#9A#s&KA|");

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(userModerator, isPersistent: false);
                    await _userManager.AddToRoleAsync(userModerator, Roles.Admin.ToString());
                    return RedirectToAction("Index", "Scheduler");
                }
            }

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    //adding user to cache
                    return RedirectToAction("Index", "Scheduler");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }
        #endregion
        /**************************************************************************************/
        #region Register
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterViewModel model)
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
                    if (model.NewMedic)
                    {
                        await _medicsManager.CreateAsync(new MedicViewModel() { Name = model.MedicName, Mail = model.Email });
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        await _userManager.AddToRoleAsync(user, Roles.Nurse.ToString());
                        return RedirectToAction("index", "Home");
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                        return RedirectToAction("index", "Home");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
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

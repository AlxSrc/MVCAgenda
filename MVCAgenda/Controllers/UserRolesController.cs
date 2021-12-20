using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Models.Accounts.Roles;
using MVCAgenda.Service.Medics;

namespace MVCAgenda.Controllers
{
    [Authorize(Roles = "Admin,Administrator")]
    public class UserRolesController : Controller
    {
        #region Fields

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMedicService _medicsService;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public UserRolesController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IMedicService medicsService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _medicsService = medicsService;
        }

        #endregion

        /***********************************************************************************/

        #region Methods

        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _userManager.Users.Where(u => u.Email != Constants.AdminUser).ToListAsync();
                var userRolesViewModel = new List<UserRolesViewModel>();

                var medics = await _medicsService.GetListAsync();

                foreach (var user in users)
                {
                    var thisViewModel = new UserRolesViewModel();
                    thisViewModel.UserId = user.Id;
                    thisViewModel.Email = user.Email;
                    thisViewModel.Roles = await GetUserRoles(user);

                    //Adding description to user roles
                    //designation
                    var medic = medics.FirstOrDefault(m => m.Mail.ToUpper() == user.Email.ToUpper());
                    if (medic != null)
                    {
                        medic.Designation = string.Join(", ", thisViewModel.Roles.ToList());
                        await _medicsService.UpdateAsync(medic);
                    }
                    
                    userRolesViewModel.Add(thisViewModel);
                }

                var viewModel = new UsersRolesViewModel() { Users = userRolesViewModel };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return View(new UsersRolesViewModel() { Users = new List<UserRolesViewModel>() });
            }
        }

        private async Task<List<string>> GetUserRoles(IdentityUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));

        }

        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }

                model.Add(userRolesViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return null;
                }

                var user = await _userManager.FindByIdAsync(id);
                if(user!=null)
                {
                var deleteRepsonse = await _userManager.DeleteAsync(user);
                    if (deleteRepsonse.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        #endregion
    }
}
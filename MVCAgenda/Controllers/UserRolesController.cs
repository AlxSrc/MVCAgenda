﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Users;
using MVCAgenda.Models.Accounts.Roles;

namespace MVCAgenda.Controllers
{
    public class UserRolesController : Controller
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        #endregion
        /***********************************************************************************/
        #region Constructor
        public UserRolesController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        #endregion
        /***********************************************************************************/
        #region Methods
        public async Task<IActionResult> Index()
        {

            if (User.Identity.IsAuthenticated)
            {

                var users = await _userManager.Users.ToListAsync();
                var userRolesViewModel = new List<UserRolesViewModel>();
                foreach (var user in users)
                {
                    var thisViewModel = new UserRolesViewModel();
                    thisViewModel.UserId = user.Id;
                    thisViewModel.Email = user.Email;
                    //thisViewModel.FirstName = user.FirstName;
                    //thisViewModel.LastName = user.LastName;
                    thisViewModel.Roles = await GetUserRoles(user);
                    userRolesViewModel.Add(thisViewModel);
                }

                var viewModel = new UsersRolesViewModel() { Users = userRolesViewModel };

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        private async Task<List<string>> GetUserRoles(IdentityUser user)
        {

            if (User.Identity.IsAuthenticated)
            {
                return new List<string>(await _userManager.GetRolesAsync(user));

            }
            else
            {
                return null;
            }
        }

        public async Task<IActionResult> Manage(string userId)
        {

            if (User.Identity.IsAuthenticated)
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
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        {

            if (User.Identity.IsAuthenticated)
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
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Users;
using MVCAgenda.Models.Accounts.Roles;
using System.Threading.Tasks;

namespace MVCAgenda.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleManagerController : Controller
    {
        #region Fields

        private readonly RoleManager<IdentityRole> _roleManager;

        #endregion

        /******************************************************************************************/

        #region Constructor

        public RoleManagerController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        #endregion

        /******************************************************************************************/

        #region Methods

        public async Task<IActionResult> Index()
        {
            var viewModel = new RolesManagerViewModel();
            viewModel.RolesList = await _roleManager.Roles.ToListAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string userId)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(userId);

            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    return null;
            }
            else
                return null;
        }

        #endregion
    }
}
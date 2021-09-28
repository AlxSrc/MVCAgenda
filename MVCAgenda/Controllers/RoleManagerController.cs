using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Models.Accounts.Roles;
using System.Threading.Tasks;

namespace MVCAgenda.Controllers
{
    //[Authorize(Roles = "Manager")]
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

        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string userId)
        {
            var roleToDelete = await _roleManager.FindByIdAsync(userId);
            if (roleToDelete != null)
            {
                await _roleManager.DeleteAsync(roleToDelete);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}

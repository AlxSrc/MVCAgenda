using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MVCAgenda.Core.Users;
using MVCAgenda.Core.Users.AppPermissions;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCAgenda.Data.DataBaseManager.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedBasicNurseAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                var defaultNurse = new IdentityUser
                {
                    UserName = "basinurse@gmail.com",
                    Email = "basinurse@gmail.com",
                    EmailConfirmed = true
                };
                if (userManager.Users.Any() != false)
                {
                    var user = await userManager.FindByEmailAsync(defaultNurse.Email);
                    if (user == null)
                    {
                        await userManager.CreateAsync(defaultNurse, "C|!nk@ $3rNN");
                        await userManager.AddToRoleAsync(defaultNurse, Roles.Nurse.ToString());
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
        public static async Task SeedModeratorAdminAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                var moderatorUser = new IdentityUser
                {
                    UserName = "moderator_agenda@gmail.com",
                    Email = "moderator_agenda@gmail.com",
                    EmailConfirmed = true
                };
                if (userManager.Users.Any() != false)
                {
                    var user = await userManager.FindByEmailAsync(moderatorUser.Email);
                    if (user == null)
                    {
                        await userManager.CreateAsync(moderatorUser, "{Al@ka#9A#s&KA|");
                        await userManager.AddToRoleAsync(moderatorUser, Roles.Moderator.ToString());
                        await userManager.AddToRoleAsync(moderatorUser, Roles.Administrator.ToString());
                    }
                    await roleManager.SeedClaimsForModeratorAdmin();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async static Task SeedClaimsForModeratorAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Moderator");
            await roleManager.AddPermissionClaim(adminRole, "Patients");
            await roleManager.AddPermissionClaim(adminRole, "PatientSheets");
            await roleManager.AddPermissionClaim(adminRole, "Consultations");
            await roleManager.AddPermissionClaim(adminRole, "Appointments");
            await roleManager.AddPermissionClaim(adminRole, "Rooms");
            await roleManager.AddPermissionClaim(adminRole, "Medics");
        }

        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MVCAgenda.Models.Accounts.ManageAccount;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.ManageAccount
{
    public class ManageAccountManager : UserManager<IdentityUser>,IManageAccountManager
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        #endregion
        /*******************************************************************************************/
        #region Constructor

        public ManageAccountManager(
            IUserStore<IdentityUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<IdentityUser> passwordHasher,
            IEnumerable<IUserValidator<IdentityUser>> userValidators,
            IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<IdentityUser>> logger,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion
        /*******************************************************************************************/
        #region Methods
        public async Task<ProfileViewModel> GetUserProfileAsync(string accountId)
        {
            var profile = new ProfileViewModel();
            try
            {
                var user = await _userManager.FindByNameAsync(accountId);
                profile.FirstName = "";
                profile.LastName = "";
                profile.Username = user.UserName;
                profile.ProfilePicture = "";

                return profile;
            }
            catch
            {
                return null;
            }
        }
        #endregion

    }
}

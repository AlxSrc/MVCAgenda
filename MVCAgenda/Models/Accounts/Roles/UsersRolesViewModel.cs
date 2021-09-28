using System.Collections.Generic;

namespace MVCAgenda.Models.Accounts.Roles
{
    public class UsersRolesViewModel
    {
        public UsersRolesViewModel()
        {
            Users = new List<UserRolesViewModel>();
        }

        #region Param
        public List<UserRolesViewModel> Users { get; set; }
        #endregion
    }
}

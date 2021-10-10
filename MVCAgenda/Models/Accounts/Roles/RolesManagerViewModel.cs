using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MVCAgenda.Models.Accounts.Roles
{
    public class RolesManagerViewModel
    {
        public RolesManagerViewModel()
        {
            RolesList = new List<IdentityRole>();
        }

        #region Param

        public List<IdentityRole> RolesList { get; set; }

        #endregion
    }
}
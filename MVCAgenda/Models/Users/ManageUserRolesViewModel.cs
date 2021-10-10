using System.Collections.Generic;

namespace MVCAgenda.Models.Users
{
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public IList<UserRolesViewModel> UserRoles { get; set; }
    }
}
using System.ComponentModel;

namespace MVCAgenda.Models.Accounts.Roles
{
    public class ManageUserRolesViewModel
    {
        public string RoleId { get; set; }

        [DisplayName("Denumirea rolului")]
        public string RoleName { get; set; }

        [DisplayName("Selectat")]
        public bool Selected { get; set; }
    }
}

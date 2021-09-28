using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Models.Accounts.Roles
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }

        [DisplayName("Nume")]
        public string FirstName { get; set; }

        [DisplayName("Prenume")]
        public string LastName { get; set; }

        [DisplayName("Username")]
        public string UserName { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Roluri")]
        public IEnumerable<string> Roles { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAgenda.Core.Helpers
{
    public class Constants
    {
        //Accounts
        public const string AdminUser = "moderator_agenda@gmail.com"; 
        public const string UserName = "moderator_agenda";

        //Appopintments
        public const int BlacklistMissingAppointmentNumber = 2;
        public const int LoyalAppointmentNumber = 10;

        //Pagination
        public const int TotalItemsOnAPage = 15;
    }
}
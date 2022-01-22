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
        public const string AdminUser = "srd.alexc@gmail.com"; 
        public const string UserName = "Alexandru";
        public const string AdminRole = "Admin";

        //Appopintments
        public const int BlacklistMissingAppointmentNumber = 2;
        public const int LoyalAppointmentNumber = 10;

        //Pagination
        public const int TotalItemsOnAPage = 45;

        //ApyKey
        public const string ApyKey = "RANDOM_TEXT_FOR_SECRET_KEY_FOR_API!)@(#{*}[$]179328";
    }
}
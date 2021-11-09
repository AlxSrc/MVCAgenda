using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAgenda.Core.Pagination
{
    public class AppointmentListItem : BaseSoftDeleteEntity
    {
        public int PatientId { get; set; }

        public string FirstName { get; set; }

        public string PhoneNumber { get; set; }

        public string Mail { get; set; }

        public string Medic { get; set; }

        public string Room { get; set; }

        public bool Made { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Procedure { get; set; }
    }
}
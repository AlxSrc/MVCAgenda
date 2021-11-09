using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAgenda.Core.Pagination
{
    public class AppointmentsPagination
    {
        public int AppointmentsCount {  get; set; }
        public int TotalPages {  get; set; }
        public IEnumerable<AppointmentListItem> Appointments {  get; set; }
    }
}

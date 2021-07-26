using MVCAgenda.Core.DomainModels;
using System.Collections.Generic;

namespace MVCAgenda.Models.MVCAgendaManagement
{
    public class ManageViewModel
    {
        public List<RoomModel> Rooms { get; set; }
        public List<MedicModel> Medics { get; set; }
    }
}

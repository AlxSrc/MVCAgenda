using System.Collections.Generic;
using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Medics;
using MVCAgenda.Models.Rooms;

namespace MVCAgenda.Models.Home
{
    public class ManageViewModel
    {
        //To Do sa schimb cu modele din viewModel
        public List<RoomViewModel> Rooms { get; set; }
        public List<MedicViewModel> Medics { get; set; }
    }
}

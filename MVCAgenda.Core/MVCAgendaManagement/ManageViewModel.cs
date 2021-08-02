using MVCAgenda.Core.Domain;
using MVCAgenda.Core.ViewModels;
using System.Collections.Generic;

namespace MVCAgenda.Core.MVCAgendaManagement
{
    public class ManageViewModel
    {
        //To Do sa schimb cu modele din viewModel
        public List<Room> Rooms { get; set; }
        public List<Medic> Medics { get; set; }
    }
}

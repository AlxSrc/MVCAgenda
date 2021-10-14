using System.Collections.Generic;

namespace MVCAgenda.Models.Rooms
{
    public class RoomsViewModel
    {
        public RoomsViewModel()
        {
            RoomsList = new List<RoomViewModel>();
        }

        #region Lists

        public List<RoomViewModel> RoomsList { get; set; }

        #endregion
    }
}

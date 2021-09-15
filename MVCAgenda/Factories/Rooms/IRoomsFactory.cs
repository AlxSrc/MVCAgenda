using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Rooms
{
    public interface IRoomsFactory
    {
        Task<RoomViewModel> PrepereRoomViewModel(Room room);
    }
}

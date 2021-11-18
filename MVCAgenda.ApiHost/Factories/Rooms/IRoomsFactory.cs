using MVCAgenda.ApiHost.DTOs.Rooms;
using MVCAgenda.Core.Domain;

namespace MVCAgenda.ApiHost.Factories.Rooms
{
    public interface IRoomsFactory
    {
        RoomDto PrepereDTO(Room room);
    }
}

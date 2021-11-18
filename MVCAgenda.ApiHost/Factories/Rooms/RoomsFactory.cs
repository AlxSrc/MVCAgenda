using MVCAgenda.ApiHost.DTOs.Rooms;
using MVCAgenda.Core.Domain;

namespace MVCAgenda.ApiHost.Factories.Rooms
{
    public class RoomsFactory : IRoomsFactory
    {
        public RoomDto PrepereDTO(Room room)
        {
            return new RoomDto()
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                PrimaryColor = room.PrimaryColor,
                SecondaryColor = room.SecondaryColor,
                Hidden = room.Hidden
            };
        }
    }
}

using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Rooms;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Rooms
{
    public class RoomsFactory : IRoomsFactory
    {
        public async Task<RoomViewModel> PrepereRoomViewModel(Room room)
        {
            return new RoomViewModel()
            {
                Id = room.Id,
                Name = room.Name,
                PrimaryColor = room.PrimaryColor,
                SecondaryColor = room.SecondaryColor,
                Description = room.Description,
                Hidden = room.Hidden
            };
        }
    }
}

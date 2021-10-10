using MVCAgenda.Models.Rooms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Rooms
{
    public interface IRoomsManager
    {
        Task<string> CreateAsync(RoomViewModel model);

        Task<List<RoomViewModel>> GetListAsync();

        Task<RoomViewModel> GetDetailsAsync(int id);

        Task<string> UpdateAsync(RoomViewModel model);

        Task<string> DeleteAsync(int id);
    }
}
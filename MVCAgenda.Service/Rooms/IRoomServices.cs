using MVCAgenda.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Rooms
{
    public interface IRoomServices
    {
        Task<bool> CreateAsync(Room room);
        
        Task<Room> GetAsync(int id);
        Task<List<Room>> GetListAsync();

        Task<bool> UpdateAsync(Room room);

        Task<bool> HideAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}

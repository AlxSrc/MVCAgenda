using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Data.DataBaseManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Rooms
{
    public class RoomServices : IRoomServices
    {
        #region Fields
        private readonly AgendaContext _context;
        #endregion
        /**************************************************************************************/
        #region Constructor
        public RoomServices(AgendaContext context)
        {
            _context = context;
        }
        #endregion
        /**************************************************************************************/
        #region Methods
        public async Task<bool> CreateAsync(Room room)
        {
            try
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public async Task<Room> GetAsync(int id)
        {
            return await _context.Rooms.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<List<Room>> GetListAsync()
        {
            return await _context.Rooms.OrderBy(r => r.Name).Where(c => c.Hidden == false).ToListAsync();
        }
        
        public async Task<bool> UpdateAsync(Room room)
        {
            try
            {
                 _context.Rooms.Update(room);
                 await _context.SaveChangesAsync();
               
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        
        public async Task<bool> HideAsync(int id)
        {
            try
            {
                var room = await _context.Rooms.FindAsync(id);
                room.Hidden = true;
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _context.Rooms.Remove(await _context.Rooms.FindAsync(id));
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}

using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Core.Logging;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Logins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Rooms
{
    public class RoomService : IRoomService
    {
        #region Fields

        private string msg;
        private readonly AgendaContext _context;
        private readonly ILoggerService _logger;
        private readonly IWorkContext _workContext;

        #endregion

        /**************************************************************************************/

        #region Constructor

        public RoomService(AgendaContext context, ILoggerService logger, IWorkContext workContext)
        {
            _context = context;
            _logger = logger;
            _workContext = workContext;
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
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Rooms}, Action: {LogInfo.Create}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return false;
            }
        }

        public async Task<Room> GetAsync(int id)
        {
            try
            {
                return await _context.Rooms.FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Rooms}, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return null;
            }
        }

        public async Task<List<Room>> GetListAsync()
        {
            try
            {
                return await _context.Rooms.OrderBy(r => r.Name).Where(c => c.Hidden == false).ToListAsync();
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Rooms}, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Room room)
        {
            try
            {
                var roomToBeEdited = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == room.Id);
                _context.Entry(roomToBeEdited).CurrentValues.SetValues(room);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Rooms}, Action: {LogInfo.Edit}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
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
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Rooms}, Action: {LogInfo.Hide}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return false;
            }
        }

        public async Task<bool> UnHideAsync(int id)
        {
            try
            {
                var room = await _context.Rooms.FindAsync(id);
                room.Hidden = false;
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Rooms}, Action: {LogInfo.UnHide}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
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
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Rooms}, Action: {LogInfo.Delete}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return false;
            }
        }

        #endregion
    }
}
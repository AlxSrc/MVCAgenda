using MVCAgenda.Core;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Core.Logging;
using MVCAgenda.Factories.Rooms;
using MVCAgenda.Models.Rooms;
using MVCAgenda.Service.Logins;
using MVCAgenda.Service.Rooms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Rooms
{
    public class RoomsManager : IRoomsManager
    {
        #region Fields

        private readonly IRoomService _roomsServices;
        private readonly IRoomsFactory _roomsFactory;
        private readonly ILoggerService _logger;
        private readonly IWorkContext _workContext;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public RoomsManager(IRoomService roomsServices, 
            IRoomsFactory roomsFactory, 
            ILoggerService loggerServices,
            IWorkContext workContext)
        {
            _roomsServices = roomsServices;
            _roomsFactory = roomsFactory;
            _logger = loggerServices;
            _workContext = workContext;
        }

        #endregion

        /***********************************************************************************/

        #region Methods

        public async Task<string> CreateAsync(RoomViewModel model)
        {
            try
            {
                var room = new Room()
                {
                    Name = model.Name,
                    PrimaryColor = model.PrimaryColor == null || model.PrimaryColor == "" ? "#7f73bb" : model.PrimaryColor,
                    SecondaryColor = model.SecondaryColor == null || model.SecondaryColor == "" ? "#f5f5f5" : model.SecondaryColor,
                    Description = model.Description,
                    Hidden = false
                };

                var result = await _roomsServices.CreateAsync(room);
                if (result == false)
                    return "Camera nu s-a putut creea";
                else
                {
                    //var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Rooms} manager, Action: {LogInfo.Read}, Room: {room.Id}";
                    //await _logger.CreateAsync(msg, null, null, LogLevel.Information);

                    return StringHelpers.SuccesMessage;
                }
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Rooms} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return "Camera nu a putut fi adaugat.";
            }
        }

        public async Task<string> UpdateAsync(RoomViewModel model)
        {
            try
            {
                if (await CheckExist(model.Id) == false)
                {
                    return "Camera nu a putut fi gasita.";
                }
                else
                {
                    var room = new Room()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        PrimaryColor = model.PrimaryColor == null || model.PrimaryColor == "" ? "#7f73bb" : model.PrimaryColor,
                        SecondaryColor = model.SecondaryColor == null || model.SecondaryColor == "" ? "#f5f5f5" : model.SecondaryColor,
                        Description = model.Description,
                        Hidden = model.Hidden
                    };

                    var result = await _roomsServices.UpdateAsync(room);
                    if (result == false)
                        return "Camera nu a putut fi editata.";
                    else
                    {
                        //var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Rooms} manager, Action: {LogInfo.Edit}, Room: {model.Id}";
                        //await _logger.CreateAsync(msg, null, null, LogLevel.Information);

                        return StringHelpers.SuccesMessage;
                    }
                }
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Rooms} manager, Action: {LogInfo.Edit}, Room: {model.Id}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return "Camera nu a putut fi editata.";
            }
        }

        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                if (await CheckExist(id) == false)
                {
                    return "Camera inexistenta.";
                }
                else
                {
                    var result = await _roomsServices.HideAsync(id);
                    if (result == false)
                        return "Camera nu a putut fi stearsa.";
                    else
                    {
                        //var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Rooms} manager, Action: {LogInfo.Hide}, Room: {id}";
                        //await _logger.CreateAsync(msg, null, null, LogLevel.Information);

                        return StringHelpers.SuccesMessage;
                    }
                }
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Rooms} manager, Action: {LogInfo.Hide}, Room: {id}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return "Camera nu a putut fi stearsa.";
            }
        }

        #endregion

        /***********************************************************************************/

        #region Utils

        private async Task<bool> CheckExist(int id)
        {
            var model = await _roomsServices.GetAsync(id);

            if (model == null)
                return false;

            return true;
        }

        #endregion
    }
}
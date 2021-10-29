using MVCAgenda.Core;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Logging;
using MVCAgenda.Models.Rooms;
using MVCAgenda.Service.Logins;
using MVCAgenda.Service.Rooms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Rooms
{
    public class RoomsFactory : IRoomsFactory
    {
        #region Fields

        private readonly IRoomService _roomsServices;
        private readonly ILoggerService _logger;
        private readonly IWorkContext _workContext;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public RoomsFactory(IRoomService roomsServices, ILoggerService loggerServices, IWorkContext workContext)
        {
            _roomsServices = roomsServices;
            _logger = loggerServices;
            _workContext = workContext;
        }

        #endregion

        /***********************************************************************************/

        #region Methods

        public async Task<List<RoomViewModel>> PrepereListViewModelAsync()
        {
            try
            {
                var rooms = await _roomsServices.GetListAsync();
                var roomsViewModel = new List<RoomViewModel>();
                foreach (var room in rooms)
                    if (room.Hidden == false)
                        roomsViewModel.Add(PrepereRoom(room));

                return roomsViewModel;
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Rooms} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new List<RoomViewModel>();
            }
        }

        public async Task<RoomsViewModel> PrepereRoomsViewModelAsync()
        {
            try
            {
                var roomsListViewModel = new List<RoomViewModel>();
                var rooms = await _roomsServices.GetListAsync();

                foreach (var room in rooms)
                    roomsListViewModel.Add(PrepereRoom(room));

                var roomsViewModel = new RoomsViewModel()
                {
                    RoomsList = roomsListViewModel
                };
                return roomsViewModel;
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new RoomsViewModel();
            }
        }

        public async Task<RoomViewModel> PrepereDetailsViewModelAsync(int id)
        {
            try
            {
                return PrepereRoom(await _roomsServices.GetAsync(id));
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Rooms} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new RoomViewModel();
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
        public static RoomViewModel PrepereRoom(Room room)
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
        #endregion
    }
}
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.ApiHost.DTOs.Appointments;
using MVCAgenda.ApiHost.DTOs.Errors;
using MVCAgenda.ApiHost.DTOs.Rooms;
using MVCAgenda.ApiHost.Factories.Appointments;
using MVCAgenda.ApiHost.Factories.Rooms;
using MVCAgenda.ApiHost.JSON.ActionResults;
using MVCAgenda.ApiHost.JSON.Serializers;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Core.Status;
using MVCAgenda.Service.Appointments;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.Rooms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
namespace MVCAgenda.ApiHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : Controller
    {
        #region Fields

        private readonly IRoomService _roomService;
        private readonly IRoomsFactory _roomsFactory;
        IJsonFieldsSerializer _jsonFieldsSerializer;

        #endregion

        #region Constructor

        public RoomsController(IRoomService roomService, IRoomsFactory roomsFactory)
        {
            _roomService = roomService;
            _roomsFactory = roomsFactory;
        }

        #endregion

        [HttpGet]
        [Route("/api/rooms", Name = "GetRooms")]
        [ProducesResponseType(typeof(RoomsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RoomsRootObject>> GetRooms()
        {
            try
            {
                var rooms = await _roomService.GetListAsync();
                var roomsAsDtos = new List<RoomDto>();
                foreach (var room in rooms)
                    roomsAsDtos.Add(_roomsFactory.PrepereDTO(room));

                var roomsRoot = new RoomsRootObject()
                {
                    Rooms = roomsAsDtos
                };

                var json = JsonConvert.SerializeObject(roomsRoot);

                return new RawJsonActionResult(json);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}

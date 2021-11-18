using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MVCAgenda.ApiHost.DTOs.Rooms
{
    public class RoomsRootObject : ISerializableObject
    {
        public RoomsRootObject()
        {
            Rooms = new List<RoomDto>();
        }

        [JsonProperty("rooms")]
        public IList<RoomDto> Rooms { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "rooms";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(RoomDto);
        }
    }
}

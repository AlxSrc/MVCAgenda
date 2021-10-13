using System;
using System.Collections.Generic;

namespace MVCAgenda.ApiHost.Maps
{
    public interface IJsonPropertyMapper
    {
        Dictionary<string, Tuple<string, Type>> GetMap(Type type);
    }
}

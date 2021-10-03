using System;

namespace MVCAgenda.ApiHost.DTOs
{
    public interface ISerializableObject
    {
        string GetPrimaryPropertyName();
        Type GetPrimaryPropertyType();
    }
}

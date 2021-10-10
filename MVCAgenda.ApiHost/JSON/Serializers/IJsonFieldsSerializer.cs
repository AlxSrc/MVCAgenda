using MVCAgenda.ApiHost.DTOs;

namespace MVCAgenda.ApiHost.JSON.Serializers
{
    public interface IJsonFieldsSerializer
    {
        string Serialize(ISerializableObject objectToSerialize, string fields);

        string Serialize(ISerializableObject objectToSerialize);
    }
}
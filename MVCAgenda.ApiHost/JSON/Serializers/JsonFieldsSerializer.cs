using System;
using System.Collections.Generic;
using System.Linq;
using MVCAgenda.ApiHost.DTOs;
using MVCAgenda.ApiHost.Helpers;
using Newtonsoft.Json.Linq;

namespace MVCAgenda.ApiHost.JSON.Serializers
{
    public class JsonFieldsSerializer : IJsonFieldsSerializer
    {
        public string Serialize(ISerializableObject objectToSerialize, string jsonFields)
        {
            if (objectToSerialize == null)
            {
                throw new ArgumentNullException(nameof(objectToSerialize));
            }

            IList<string> fieldsList = null;

            if (!string.IsNullOrEmpty(jsonFields))
            {
                var primaryPropertyName = objectToSerialize.GetPrimaryPropertyName();

                fieldsList = GetPropertiesIntoList(jsonFields);

                // Always add the root manually
                fieldsList.Add(primaryPropertyName);
            }

            var json = Serialize(objectToSerialize, fieldsList);

            return json;
        }

        public string Serialize(ISerializableObject objectToSerialize)
        {
            if (objectToSerialize == null)
            {
                throw new ArgumentNullException(nameof(objectToSerialize));
            }

            var json = Serialize(objectToSerialize);

            return json;
        }


        private string Serialize(object objectToSerialize, IList<string> jsonFields = null)
        {
            var jToken = JToken.FromObject(objectToSerialize);

            if (jsonFields != null)
            {
                jToken = jToken.RemoveEmptyChildrenAndFilterByFields(jsonFields);
            }

            var jTokenResult = jToken.ToString();

            return jTokenResult;
        }

        private IList<string> GetPropertiesIntoList(string fields)
        {
            IList<string> properties = fields.ToLowerInvariant()
                                             .Split(new[]
                                                    {
                                                        ','
                                                    }, StringSplitOptions.RemoveEmptyEntries)
                                             .Select(x => x.Trim())
                                             .Distinct()
                                             .ToList();

            return properties;
        }
    }
}

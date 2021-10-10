using MVCAgenda.ApiHost.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace MVCAgenda.ApiHost.Attributes
{
    public class FromQueryJsonAttribute : ModelBinderAttribute
    {
        public FromQueryJsonAttribute()
        {
            BinderType = typeof(JsonQueryModelBinder);
        }

        public FromQueryJsonAttribute(string paramName)
            : this()
        {
            Name = paramName;
        }
    }
}
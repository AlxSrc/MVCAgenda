
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.ApiHost.ModelBinders
{
    public class JsonQueryModelBinder : IModelBinder
    {
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			try
			{
				var name = bindingContext.ModelName;
				var json = bindingContext.HttpContext.Request.Query.FirstOrDefault(x => x.Key == name).Value;
				var targetType = bindingContext.ModelMetadata.ModelType;
				var model = JsonConvert.DeserializeObject(json, targetType);
				if (model is null)
				{
					model = Activator.CreateInstance(targetType);
				}
				bindingContext.Model = model;
				bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
			}
			catch (JsonException)
			{
				bindingContext.Result = ModelBindingResult.Failed();
			}
			return Task.CompletedTask;
		}
	}
}

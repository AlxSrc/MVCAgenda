using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.ApiHost.ModelBinders;

namespace MVCAgenda.ApiHost.Models.Parameters.Patients
{
    [ModelBinder(typeof(ParametersModelBinder<PatientsParametersModel>))]
    public class PatientsParametersModel : BasePatientsParametersModel
    {
        public PatientsParametersModel()
        {
            Fields = string.Empty;
        }

        /// <summary>
        ///     comma-separated list of fields to include in the response
        public string Fields { get; set; }
    }
}

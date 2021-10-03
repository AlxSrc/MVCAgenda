using Newtonsoft.Json;

namespace MVCAgenda.ApiHost.Models.Parameters.Patients
{
    public class PatientsParametersModel : BasePatientsParametersModel
    {
        public PatientsParametersModel()
        {
            Fields = string.Empty;
        }

        /// <summary>
        ///     comma-separated list of fields to include in the response
        /// </summary>
        [JsonProperty("fields")]
        public string Fields { get; set; }
    }
}

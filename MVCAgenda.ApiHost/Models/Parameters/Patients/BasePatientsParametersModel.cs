using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MVCAgenda.ApiHost.Models.Parameters.Patients
{
    public class BasePatientsParametersModel
    {
        public BasePatientsParametersModel()
        {
            IsDeleted = false;
            IncludeBlackList = false;
            SearchByName = string.Empty;
            SearchByPhoneNumber = string.Empty;
            SearchByEmail = string.Empty;
        }

        public bool? IsDeleted { get; set; }

        public bool? IncludeBlackList { get; set; }

        public string SearchByName { get; set; }

        public string SearchByPhoneNumber { get; set; }

        public string SearchByEmail { get; set; }
    }
}

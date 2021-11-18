using Microsoft.AspNetCore.Mvc;
using MVCAgenda.ApiHost.DTOs.Appointments;
using MVCAgenda.ApiHost.DTOs.Errors;
using MVCAgenda.ApiHost.DTOs.Medics;
using MVCAgenda.ApiHost.Factories.Appointments;
using MVCAgenda.ApiHost.Factories.Medics;
using MVCAgenda.ApiHost.JSON.ActionResults;
using MVCAgenda.ApiHost.JSON.Serializers;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Core.Status;
using MVCAgenda.Service.Appointments;
using MVCAgenda.Service.Medics;
using MVCAgenda.Service.Patients;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MVCAgenda.ApiHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicsController : Controller
    {

        #region Fields

        private readonly IMedicService _medicService;
        private readonly IMedicsFactory _medicsFactory;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        #endregion

        #region Constructor

        public MedicsController(IMedicService medicService, IMedicsFactory medicsFactory)
        {
            _medicService = medicService;
            _medicsFactory = medicsFactory;
        }

        #endregion

        [HttpGet]
        [Route("/api/medics", Name = "GetMedicss")]
        [ProducesResponseType(typeof(MedicsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AppointmentsRootObject>> GetAppointments()
        {
            try
            {
                var medics = await _medicService.GetListAsync(hidden:false);
                var medicsAsDtos = new List<MedicDto>();
                foreach (var medic in medics)
                    medicsAsDtos.Add(_medicsFactory.PrepereDTO(medic));

                var medicsRoot = new MedicsRootObject()
                {
                    Medics = medicsAsDtos
                };

                var json = JsonConvert.SerializeObject(medicsRoot);

                return new RawJsonActionResult(json);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MVCAgenda.ApiHost.DTOs.Appointments;
using MVCAgenda.ApiHost.DTOs.Errors;
using MVCAgenda.ApiHost.Factories.Appointments;
using MVCAgenda.ApiHost.JSON.ActionResults;
using MVCAgenda.ApiHost.JSON.Serializers;
using MVCAgenda.Service.Appointments;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MVCAgenda.ApiHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : Controller
    {

        #region Fields

        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentsFactory _appointmentsFactory;
        IJsonFieldsSerializer _jsonFieldsSerializer;

        #endregion

        #region Constructor

        public AppointmentsController(IAppointmentService appointmentService, IAppointmentsFactory appointmentsFactory)
        {
            _appointmentService = appointmentService;
            _appointmentsFactory = appointmentsFactory;
        }

        #endregion

        [HttpGet]
        [Route("/api/appointments", Name = "GetAppointments")]
        [ProducesResponseType(typeof(AppointmentsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AppointmentsRootObject>> GetPatients()
        {
            try
            {
                var appointments = await _appointmentService.GetFiltredListAsync();
                var appointmentsAsDtos = new List<AppointmentDto>();
                foreach (var appointment in appointments)
                    appointmentsAsDtos.Add(await _appointmentsFactory.PrepereAppointmentDTO(appointment));

                var appointmentsRoot = new AppointmentsRootObject()
                {
                    Appointments = appointmentsAsDtos
                };

                var json = JsonConvert.SerializeObject(appointmentsRoot);

                return new RawJsonActionResult(json);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}

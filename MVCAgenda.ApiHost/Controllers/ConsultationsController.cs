using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.ApiHost.DTOs.Consultations;
using MVCAgenda.ApiHost.DTOs.Errors;
using MVCAgenda.ApiHost.Factories.Consultations;
using MVCAgenda.ApiHost.JSON.ActionResults;
using MVCAgenda.ApiHost.JSON.Serializers;
using MVCAgenda.Core.Domain;
using MVCAgenda.Service.Consultations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MVCAgenda.ApiHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ConsultationsController : Controller
    {

        #region Fields

        private readonly IConsultationService _consultationService;
        private readonly IConsultationFactory _consultationFactory;
        IJsonFieldsSerializer _jsonFieldsSerializer;

        #endregion

        #region Constructor

        public ConsultationsController(IConsultationService consultationService, IConsultationFactory consultationFactory)
        {
            _consultationService = consultationService;
            _consultationFactory = consultationFactory;
        }

        #endregion

        //[HttpGet]
        //[ProducesResponseType(typeof(ConsultationRootObject), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult<ConsultationRootObject>> GetConsultations()
        //{
        //    try
        //    {
        //        var appointments = await _appointmentService.GetFiltredListAsync(-1, Daily: true);
        //        var appointmentsAsDtos = new List<AppointmentCompleteDataDto>();
        //        foreach (var appointment in appointments)
        //            appointmentsAsDtos.Add(await _appointmentsFactory.PrepereAppointmentDTO(appointment));

        //        var appointmentsRoot = new AppointmentsRootObject()
        //        {
        //            Appointments = appointmentsAsDtos
        //        };

        //        var json = JsonConvert.SerializeObject(appointmentsRoot);

        //        return new RawJsonActionResult(json);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest();
        //    }
        //}


        [HttpGet]
        [Route("/api/Consultations/{id}", Name = "GetConsultationById")]
        [ProducesResponseType(typeof(ConsultationRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetConsultationById(int id, string fields = "")
        {
            try
            {
                var consultation = await _consultationService.GetAsync(id);

                var consultationsAsDtos = new List<ConsultationDto>();
                consultationsAsDtos.Add(_consultationFactory.PrepereConsultationDTO(consultation));

                var consultationsRoot = new ConsultationRootObject()
                {
                    Consultations = consultationsAsDtos
                };

                var json = JsonConvert.SerializeObject(consultationsRoot);

                return new RawJsonActionResult(json);

            }
            catch
            {
                return BadRequest();
            }
        }


        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ConsultationRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutConsultation(int id, ConsultationDto consultation)
        {
            if (id != consultation.Id)
            {
                return BadRequest();
            }

            try
            {
                var newConsultation = new Consultation()
                {
                    Id = consultation.Id,
                    PatientSheetId = consultation.PatientSheetId,
                    Prescriptions = consultation.Prescriptions,
                    Symptoms = consultation.Symptoms,
                    CreationDate = consultation.CreationDate,
                    Diagnostic = consultation.Diagnostic,
                    Hidden = consultation.Hidden
                };
                var ressult = await _consultationService.UpdateAsync(newConsultation);
                if (ressult == true)
                    return Ok();
                else
                    return BadRequest();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(ConsultationRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostConsultation(ConsultationDto consultation)
        {
            try
            {
                var newConsultation = new Consultation()
                {
                    PatientSheetId = consultation.PatientSheetId,
                    Prescriptions = consultation.Prescriptions,
                    Symptoms = consultation.Symptoms,
                    CreationDate = consultation.CreationDate,
                    Diagnostic = consultation.Diagnostic,
                    Hidden = consultation.Hidden
                };
                var ressult = await _consultationService.CreateAsync(newConsultation);
                if (ressult == true)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ConsultationRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteConsultation(int id)
        {
            var response = await _consultationService.DeleteAsync(id);
            if (response == true)
                return Ok();
            return BadRequest();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MVCAgenda.ApiHost.DTOs.Appointments;
using MVCAgenda.ApiHost.DTOs.Errors;
using MVCAgenda.ApiHost.Factories.Appointments;
using MVCAgenda.ApiHost.JSON.ActionResults;
using MVCAgenda.ApiHost.JSON.Serializers;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Core.Status;
using MVCAgenda.Service.Appointments;
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
    public class AppointmentsController : Controller
    {

        #region Fields

        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentsFactory _appointmentsFactory;
        private readonly IPatientService _patientService;
        IJsonFieldsSerializer _jsonFieldsSerializer;

        #endregion

        #region Constructor

        public AppointmentsController(IAppointmentService appointmentService, IAppointmentsFactory appointmentsFactory, IPatientService patientService)
        {
            _appointmentService = appointmentService;
            _appointmentsFactory = appointmentsFactory;
            _patientService = patientService;
        }

        #endregion

        [HttpGet]
        [Route("/api/appointments", Name = "GetAppointments")]
        [ProducesResponseType(typeof(AppointmentsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AppointmentsRootObject>> GetAppointments()
        {
            try
            {
                var appointments = await _appointmentService.GetFiltredListAsync(-1, Daily: true);
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

        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AppointmentsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutPatient(int id, AppointmentDto appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }

            try
            {
                var newAppointment = new Appointment()
                {
                    Id = appointment.Id,

                    PatientId = appointment.PatientId,
                    RoomId = appointment.RoomId,
                    MedicId = appointment.MedicId,

                    Procedure = appointment.Procedure,
                    Comments = appointment.Comments,
                    StartDate = appointment.StartDate,
                    EndDate = appointment.EndDate,
                    
                    Made = appointment.Made,

                    AppointmentCreationDate = appointment.AppointmentCreationDate,
                    ResponsibleForAppointment = appointment.ResponsibleForAppointment,
                    Hidden = false
                };
                var ressult = await _appointmentService.UpdateAsync(newAppointment);
                if(ressult == true)
                    return Ok();
                else
                    return BadRequest();

            }
            catch
            {
                return BadRequest();
            }

        }

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(AppointmentsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostAppointment(AppointmentDto appointment)
        {
            try
            {
                int patientId;

                var newPatient = new Patient
                {
                    FirstName = appointment.FirstName,
                    LastName = appointment.LastName,
                    PhoneNumber = appointment.PhoneNumber,
                    Mail = appointment.Mail
                };

                patientId = await _patientService.CheckExistentPatientAsync(newPatient);

                var newAppointment = new Appointment
                {
                    PatientId = patientId,
                    MedicId = appointment.MedicId,
                    RoomId = appointment.RoomId,
                    StartDate = appointment.StartDate,
                    EndDate = appointment.EndDate,
                    Procedure = appointment.Procedure,
                    Made = true,
                    ResponsibleForAppointment = appointment.ResponsibleForAppointment,
                    AppointmentCreationDate = DateTime.Now,
                    Comments = appointment.Comments,
                    Hidden = false
                };

                var result = await _appointmentService.CreateAsync(newAppointment);
                if (result == false)
                    return BadRequest();
                else
                {
                    //folosind id-ul pacientului din programarea adaugata
                    patientId = newAppointment.PatientId;
                    //aduc toate programarile pacientului respectiv
                    var appointments = await _appointmentService.GetFiltredListAsync(1, id: patientId, Hidden: false);
                    //verific daca are 10 programari efectuate si in cazul pozitiv ii dau titlul de pacient fidel
                    if (appointments.Count >= Constants.LoyalAppointmentNumber)
                    {
                        var patient = await _patientService.GetAsync(patientId);
                        patient.StatusCode = (int)PatientStatus.LoyalPatient;
                        var updateRessult = await _patientService.UpdateAsync(patient);
                    }
                    return Ok();
                }
            }
            catch (Exception exception)
            {
                return BadRequest();
            }
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AppointmentsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var response = await _appointmentService.DeleteAsync(id);
            if (response == true)
                return Ok();
            return BadRequest();
        }
    }
}

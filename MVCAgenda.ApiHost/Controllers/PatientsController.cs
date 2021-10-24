using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.ApiHost.DTOs.Errors;
using MVCAgenda.ApiHost.DTOs.Patients;
using MVCAgenda.ApiHost.Factories.Patients;
using MVCAgenda.ApiHost.JSON.ActionResults;
using MVCAgenda.ApiHost.JSON.Serializers;
using MVCAgenda.ApiHost.Models.Parameters.Patients;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Status;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Patients;
using Newtonsoft.Json;

namespace MVCAgenda.ApiHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : Controller
    {
        #region Fields

        private readonly AgendaContext _context;
        private readonly IPatientService _patientService;
        private readonly IPatientsFactory _patientFactory;
        IJsonFieldsSerializer _jsonFieldsSerializer;

        #endregion

        #region Constructor

        public PatientsController(AgendaContext context,
            IPatientService patientService,
            IPatientsFactory patientFactory)
        {
            _context = context;
            _patientService = patientService;
            _patientFactory = patientFactory;
        }

        #endregion


        [HttpGet]
        [Route("/api/Patients", Name = "GetPatients")]
        [ProducesResponseType(typeof(PatientsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPatients(PatientsParametersModel parameters)
        {
            try
            {
                var patients = await _patientService.GetListAsync(parameters.SearchByName,parameters.SearchByPhoneNumber,parameters.SearchByEmail, parameters.IncludeBlackList, parameters.IsDeleted);
                var patientsAsDtos = new List<PatientDto>();
                foreach (var patient in patients)
                    patientsAsDtos.Add(_patientFactory.PreperePatientDTO(patient));

                var patientsRoot = new PatientsRootObject()
                {
                    Patients = patientsAsDtos
                };

                var json = JsonConvert.SerializeObject(patientsRoot);

                return new RawJsonActionResult(json);
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        ///     Retrieve patient by specified id
        /// </summary>
        /// <param name="id">Id of the category</param>
        /// <param name="fields">Fields from the patient you want your json to contain</param>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("/api/Patients/{id}", Name = "GetPatientById")]
        [ProducesResponseType(typeof(PatientsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPatientById(int id, string fields = "")
        {
            try
            {
                var patient = await _patientService.GetAsync(id);

                var patientsAsDtos = new List<PatientDto>();
                patientsAsDtos.Add(_patientFactory.PreperePatientDTO(patient));

                var patientsRoot = new PatientsRootObject()
                {
                    Patients = patientsAsDtos
                };

                var json = JsonConvert.SerializeObject(patientsRoot);

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
        [ProducesResponseType(typeof(PatientsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutPatient(int id, PatientDto patientDto)
        {
            if (id != patientDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var patient = new Patient()
                {
                    Id = patientDto.Id,

                    FirstName = patientDto.FirstName,
                    LastName = patientDto.LastName,
                    PhoneNumber = patientDto.PhoneNumber,
                    Mail = patientDto.Mail,

                    StatusCode = patientDto.StatusCode,
                    Hidden = false
                };
                var ressult = await _patientService.UpdateAsync(patient);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(typeof(PatientsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostPatient(PatientDto patient)
        {
            var newPatient = new Patient()
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                Mail = patient.Mail,
                StatusCode = (int)PatientStatus.Patient,
                Hidden = false
            };
            var ressult = await _patientService.CreateAsync(newPatient);
            if (ressult == true)
                return Ok();
            else
                return BadRequest();
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PatientsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var response = await _patientService.DeleteAsync(id);
            return NoContent();
        }

    }
}
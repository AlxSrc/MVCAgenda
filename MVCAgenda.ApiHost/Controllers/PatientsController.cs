using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.ApiHost.Attributes;
using MVCAgenda.ApiHost.DTOs.Errors;
using MVCAgenda.ApiHost.DTOs.Patients;
using MVCAgenda.ApiHost.Factories.Patients;
using MVCAgenda.ApiHost.JSON.ActionResults;
using MVCAgenda.ApiHost.JSON.Serializers;
using MVCAgenda.ApiHost.Models.Parameters.Patients;
using MVCAgenda.Core.Domain;
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
        [Route("/api/patients", Name = "GetPatients")]
        [ProducesResponseType(typeof(PatientsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PatientsRootObject>> GetPatients()
        {
            try
            {
                var patients = await _patientService.GetListAsync();
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
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [Route("/api/patients/{id}", Name = "GetPatientById")]
        [ProducesResponseType(typeof(PatientsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPatientById(int id, string fields = "")
        {
            return null;
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}
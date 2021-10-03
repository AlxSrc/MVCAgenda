using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.ApiHost.DTOs.Errors;
using MVCAgenda.ApiHost.DTOs.Patients;
using MVCAgenda.ApiHost.Factories.Patients;
using MVCAgenda.ApiHost.JSON.ActionResults;
using MVCAgenda.ApiHost.JSON.Serializers;
using MVCAgenda.ApiHost.Models.Parameters.Patients;
using MVCAgenda.Core.Domain;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Patients;

namespace MVCAgenda.ApiHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly AgendaContext _context;
        private readonly IPatientService _patientService;
        private readonly IPatientsFactory _patientFactory;
        IJsonFieldsSerializer _jsonFieldsSerializer;

        public PatientsController(AgendaContext context, 
            IPatientService patientService, 
            IPatientsFactory patientFactory,
            IJsonFieldsSerializer jsonFieldsSerializer)
        {
            _context = context;
            _patientService = patientService;
            _patientFactory = patientFactory;
            _jsonFieldsSerializer = jsonFieldsSerializer;
        }

        // GET: api/Patients
        //[HttpGet]
        //public async Task<ActionResult<List<PatientDTO>>> GetPatients()
        //{
        //    var patients = await _patientService.GetListAsync(null, null, null, false, false);
        //    var patientsDTO = new List<PatientDTO>();

        //    foreach (var patient in patients)
        //        patientsDTO.Add(_patientFactory.PreperePatientDTO(patient));

        //    return patientsDTO;
        //}

        [HttpGet]
        [Route("/api/patients", Name = "GetPatients")]
        [ProducesResponseType(typeof(PatientsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPatients(PatientsParametersModel parameters)
        {
            var patients = await _patientService.GetListAsync(parameters.SearchByName, parameters.SearchByPhoneNumber, parameters.SearchByEmail, parameters.IncludeBlackList, parameters.IsDeleted);

            IList<PatientDto> patientsAsDtos = new List<PatientDto>();

            foreach (var patient in patients)
                patientsAsDtos.Add(_patientFactory.PreperePatientDTO(patient));

            var patientsRootObject = new PatientsRootObject
            {
                Patients = patientsAsDtos
            };

            var json = _jsonFieldsSerializer.Serialize(patientsRootObject, parameters.Fields);

            return new RawJsonActionResult(json);
        }
        
        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
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

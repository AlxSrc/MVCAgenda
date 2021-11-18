using Microsoft.AspNetCore.Mvc;
using MVCAgenda.ApiHost.DTOs.Errors;
using MVCAgenda.ApiHost.DTOs.PatientSheets;
using MVCAgenda.ApiHost.Factories.PatientSheets;
using MVCAgenda.ApiHost.JSON.ActionResults;
using MVCAgenda.ApiHost.JSON.Serializers;
using MVCAgenda.Core.Domain;
using MVCAgenda.Service.PatientsSheet;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace MVCAgenda.ApiHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientSheetsController : Controller
    {
        #region Fields

        private readonly IPatientSheetService _patientSheetService;
        private readonly IPatientSheetFactory _patientSheetFactory;
        IJsonFieldsSerializer _jsonFieldsSerializer;

        #endregion

        #region Constructor

        public PatientSheetsController(IPatientSheetService patientSheetService, IPatientSheetFactory patientSheetFactory)
        {
            _patientSheetService = patientSheetService;
            _patientSheetFactory = patientSheetFactory;
        }

        #endregion

        /// <summary>
        ///     Retrieve patient by specified id
        /// </summary>
        /// <param name="id">Id of the category</param>
        /// <param name="fields">Fields from the patient you want your json to contain</param>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("/api/PatientSheets/{id}", Name = "GetPatientSheetById")]
        [ProducesResponseType(typeof(PatientSheetRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPatientSheetById(int id, string fields = "")
        {
            try
            {
                var patientSheet = await _patientSheetFactory.PrepereDtoAsync(id);
                var patientSheetRoot = new PatientSheetRootObject()
                {
                    PatientSheet = patientSheet
                };

                var json = JsonConvert.SerializeObject(patientSheetRoot);

                return new RawJsonActionResult(json);

            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PatientSheetRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutPatientSheet(int id, PatientSheetDto patientSheetDto)
        {
            if (id != patientSheetDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var patientSheet = new PatientSheet() {
                    Id = patientSheetDto.Id,
                    PatientId = patientSheetDto.PatientId,
                    AntecedentsH = patientSheetDto.AntecedentsH,
                    AntecedentsP = patientSheetDto.AntecedentsP,
                    DateOfBirth = patientSheetDto.DateOfBirth,
                    Gender = patientSheetDto.Gender,
                    NationalIdentificationNumber = patientSheetDto.NationalIdentificationNumber,
                    PhysicalExamination = patientSheetDto.PhysicalExamination,
                    Street = patientSheetDto.Street,
                    Town = patientSheetDto.Town,
                    Hidden = patientSheetDto.Hidden
                };
                var ressult = await _patientSheetService.UpdateAsync(patientSheet);
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
    }
}

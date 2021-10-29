using Microsoft.AspNetCore.Mvc;
using MVCAgenda.ApiHost.DTOs.Errors;
using MVCAgenda.ApiHost.DTOs.Items;
using MVCAgenda.ApiHost.Factories.Items;
using MVCAgenda.ApiHost.JSON.ActionResults;
using MVCAgenda.ApiHost.Managers;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MVCAgenda.ApiHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        #region Fields

        private readonly IItemsFactory _itemsFactory;
        private readonly IItemsManager _itemsManager;
        private readonly string _path = "C:\\Users\\User\\Desktop\\Agenda\\items.json";

        #endregion

        #region Constructor

        public ItemsController(IItemsFactory itemsFactory, IItemsManager itemsManager)
        {
            _itemsFactory = itemsFactory;
            _itemsManager = itemsManager;
        }

        #endregion

        [HttpGet]
        [Route("/api/items", Name = "GetItems")]
        [ProducesResponseType(typeof(ItemsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ItemsRootObject>> GetItems()
        {
            try
            {
                var root = new ItemsRootObject();
                
                root.Patients = await _itemsFactory.PreperePatientsList();
                root.PatientSheets = await _itemsFactory.PreperePatientSheetsList();
                root.Consultations = await _itemsFactory.PrepereConsultationsList();
                root.Appointments = await _itemsFactory.PrepereAppointmentsList();


                var jsonToWrite = JsonConvert.SerializeObject(root, Formatting.Indented);
                var json = JsonConvert.SerializeObject(root); 
                using (var writer = new StreamWriter(_path))
                {
                    writer.Write(jsonToWrite);
                }


                return new RawJsonActionResult(json);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/api/items", Name = "GetItems")]
        [ProducesResponseType(typeof(ItemsRootObject), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorsRootObject), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ItemsRootObject>> PostItems()
        {
            try
            {
                var root = new ItemsRootObject();
                string jsonFromFile;

                using (var reader = new StreamReader(_path))
                {
                    jsonFromFile = reader.ReadToEnd();
                }
                root = JsonConvert.DeserializeObject<ItemsRootObject>(jsonFromFile);
                var ressult = await _itemsManager.PostData(root);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}

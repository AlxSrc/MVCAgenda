using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Factories.Medics;
using MVCAgenda.Factories.Rooms;
using MVCAgenda.Managers.Medics;
using MVCAgenda.Managers.Rooms;
using MVCAgenda.Managers.Scheduler;
using MVCAgenda.Models.SyncfusionScheduler;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Syncfusion.EJ2;
using Syncfusion.EJ2.Base;

namespace MVCAgenda.Controllers
{
    [Authorize]
    public class SchedulerController : Controller
    {
        #region Fields

        private readonly ISchedulerManager _schedulerManager;
        private readonly IRoomsManager _roomsManager;
        private readonly IMedicsManager _medicsManager;
        private readonly IRoomsFactory _roomsFactory;
        private readonly IMedicsFactory _medicsFactory;

        #endregion

        /*********************************************************************************/

        #region Constructor

        public SchedulerController(ISchedulerManager schedulerManager, 
            IRoomsManager roomsManager, 
            IMedicsManager medicsManager,
            IRoomsFactory roomsFactory,
            IMedicsFactory medicsFactory)
        {
            _schedulerManager = schedulerManager;
            _roomsManager = roomsManager;
            _medicsManager = medicsManager;
            _roomsFactory = roomsFactory;
            _medicsFactory = medicsFactory;
        }

        #endregion

        /**************************************************************************************/

        #region Create

        [HttpPost]
        public async Task<JsonResult> AddData(ScheduleEventData ScheduleData)
        {
            ScheduleData.User = User.Identity.Name;
            var ressult = await _schedulerManager.CreateAsync(ScheduleData);
            return new JsonResult(new
            {
                result = ressult,
            }, new JsonSerializerOptions());
        }

        #endregion

        /*********************************************************************************/

        #region Read

        public async Task<IActionResult> Index(string Mail)
        {
            ViewData["RoomId"] = JsonConvert.SerializeObject(await _roomsFactory.PrepereListViewModelAsync(), new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            ViewData["MedicId"] = JsonConvert.SerializeObject(await _medicsFactory.PrepereListModel(hidden: false), new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            ViewData["Mails"] = new SelectList(await _medicsFactory.PrepereListModel(hidden: false), "Mail", "Name");

            ViewBag.Employees = JsonConvert.SerializeObject(await _medicsFactory.PrepereListModel(Mail, false), new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            ViewBag.Resources = new string[] { "Employee" };

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> LoadData([FromBody]ScheduleLoadDataInputModel model)
        {
            try
            {
                var user = User.Identity.Name;
                if (user == Constants.UserName)
                {
                    var appointmentsList = await _schedulerManager.GetAsync(searchByAppointmentStartDate: model.Value.StartDate, searchByAppointmentEndDate: model.Value.EndDate,null);

                    return new JsonResult(new
                    {
                        result = appointmentsList.AppointmentsSchedule,
                    }, new JsonSerializerOptions());
                }
                else
                {
                    var appointmentsList = await _schedulerManager.GetAsync(searchByAppointmentStartDate: model.Value.StartDate, searchByAppointmentEndDate: model.Value.EndDate, null);
                    return new JsonResult(new
                    {
                        result = appointmentsList.AppointmentsSchedule,
                    }, new JsonSerializerOptions());
                }
            }
            catch
            {
                return new JsonResult(new
                {
                    Items = new List<ScheduleEventData>(),
                });
            }
        }

        #endregion

        /**************************************************************************************/

        #region Update

        [HttpPost]
        public async Task<JsonResult> EditData(ScheduleEventData ScheduleData)
        {
            ScheduleData.User = User.Identity.Name;
            var ressult = await _schedulerManager.UpdateAsync(ScheduleData);
            return new JsonResult(new
            {
                result = ressult,
            }, new JsonSerializerOptions());
        }

        #endregion

        /**************************************************************************************/

        #region Delete

        [HttpPost]
        public async Task<JsonResult> DeleteData(ScheduleEventData ScheduleData)
        {
            var ressult = await _schedulerManager.HideAsync(ScheduleData.Id);
            return new JsonResult(new
            {
                result = ressult,
            }, new JsonSerializerOptions());
        }

        #endregion
    }
}
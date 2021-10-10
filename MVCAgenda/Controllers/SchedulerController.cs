using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAgenda.Managers.Medics;
using MVCAgenda.Managers.Rooms;
using MVCAgenda.Managers.Scheduler;
using MVCAgenda.Models.SyncfusionScheduler;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MVCAgenda.Controllers
{
    [Authorize]
    public class SchedulerController : Controller
    {
        #region Services

        private readonly ISchedulerManager _schedulerManager;
        private readonly IRoomsManager _roomsManager;
        private readonly IMedicsManager _medicsManager;

        #endregion

        /*********************************************************************************/

        #region Constructor

        public SchedulerController(ISchedulerManager schedulerManager, IRoomsManager roomsManager, IMedicsManager medicsManager)
        {
            _schedulerManager = schedulerManager;
            _roomsManager = roomsManager;
            _medicsManager = medicsManager;
        }

        #endregion

        /**************************************************************************************/

        #region Create

        [HttpPost]
        public async Task<JsonResult> AddData(ScheduleEventData ScheduleData)
        {
            if (User.Identity.IsAuthenticated)
            {
                ScheduleData.User = User.Identity.Name;
                var ressult = await _schedulerManager.CreateAsync(ScheduleData);
                return new JsonResult(new
                {
                    result = ressult,
                }, new JsonSerializerOptions());
            }
            else
            {
                var ressult = "Pentru a adauga o programare trebuie sa fiti autenificat.";
                return new JsonResult(new
                {
                    result = ressult,
                }, new JsonSerializerOptions());
            }
        }

        #endregion

        /*********************************************************************************/

        #region Read

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["RoomId"] = JsonConvert.SerializeObject(await _roomsManager.GetListAsync(), new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
                ViewData["MedicId"] = JsonConvert.SerializeObject(await _medicsManager.GetListAsync(), new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                ViewBag.Employees = JsonConvert.SerializeObject(await _medicsManager.GetListAsync(), new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                ViewBag.Resources = new string[] { "Employee" };
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<JsonResult> LoadData()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var appointmentsList = await _schedulerManager.GetAsync();
                    return new JsonResult(new
                    {
                        result = appointmentsList.AppointmentsSchedule,
                    }, new JsonSerializerOptions());
                }
                catch
                {
                    return new JsonResult(new
                    {
                        Items = new List<ScheduleEventData>(),
                    });
                }
            }

            {
                var ressult = "Trebuie sa fiti conectati.";
                return new JsonResult(new
                {
                    result = ressult,
                }, new JsonSerializerOptions());
            }
        }

        #endregion

        /**************************************************************************************/

        #region Update

        [HttpPost]
        public async Task<JsonResult> EditData(ScheduleEventData ScheduleData)
        {
            if (User.Identity.IsAuthenticated)
            {
                ScheduleData.User = User.Identity.Name;
                var ressult = await _schedulerManager.UpdateAsync(ScheduleData);
                return new JsonResult(new
                {
                    result = ressult,
                }, new JsonSerializerOptions());
            }

            {
                var ressult = "Trebuie sa fiti conectati.";
                return new JsonResult(new
                {
                    result = ressult,
                }, new JsonSerializerOptions());
            }
        }

        #endregion

        /**************************************************************************************/

        #region Delete

        [HttpPost]
        public async Task<JsonResult> DeleteData(ScheduleEventData ScheduleData)
        {
            if (User.Identity.IsAuthenticated)
            {
                var ressult = await _schedulerManager.HideAsync(ScheduleData.Id);
                return new JsonResult(new
                {
                    result = ressult,
                }, new JsonSerializerOptions());
            }

            {
                var ressult = "Trebuie sa fiti conectati.";
                return new JsonResult(new
                {
                    result = ressult,
                }, new JsonSerializerOptions());
            }
        }

        #endregion
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Factories.Appointments;
using MVCAgenda.Managers.Appointments;
using MVCAgenda.Models.Appointments;
using MVCAgenda.Service.Appointments;
using MVCAgenda.Service.Consultations;
using MVCAgenda.Service.Helpers;
using MVCAgenda.Service.Medics;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.Rooms;

namespace MVCAgenda.Controllers
{
    [Authorize]
    public class HelperConvertController : Controller
    {
        #region Fields

        private readonly IAppointmentService _appointmentService; 
        private readonly IConsultationService _consultationService;
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        /**************************************************************************************/

        #region Constructor

        public HelperConvertController(
            IAppointmentService appointmentService,
            IConsultationService consultationService,
            IDateTimeHelper dateTimeHelper)
        {
            _appointmentService = appointmentService;
            _consultationService = consultationService;
            _dateTimeHelper = dateTimeHelper;
        }

        #endregion

        /**************************************************************************************/

        #region Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConvertToUtc()
        {
            var appointmentsFromDatabase = await _appointmentService.GetAppointmentListAsync();
            
            foreach(var appointment in appointmentsFromDatabase)
            {
                var appointmentToUpdate = appointment;
                appointmentToUpdate.StartDate = _dateTimeHelper.ConvertToUtcTime(appointment.StartDate);
                appointmentToUpdate.EndDate = _dateTimeHelper.ConvertToUtcTime(appointment.EndDate);
                appointmentToUpdate.AppointmentCreationDate = _dateTimeHelper.ConvertToUtcTime(appointment.AppointmentCreationDate);

                _ = await _appointmentService.UpdateAsync(appointmentToUpdate);
            }

            var consultationsFromDatabase = await _consultationService.GetListAsync(-1);

            foreach (var consultation in consultationsFromDatabase)
            {
                var consultationToUpdate = consultation;
                consultationToUpdate.CreationDate = _dateTimeHelper.ConvertToUtcTime(consultation.CreationDate);
                _ = await _consultationService.UpdateAsync(consultationToUpdate);
            }

            return RedirectToAction(nameof(Index), new { Daily = true });
        }

        #endregion
    }
}

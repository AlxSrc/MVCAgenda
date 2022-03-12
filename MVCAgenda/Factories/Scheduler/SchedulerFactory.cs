using MVCAgenda.Core;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Logging;
using MVCAgenda.Models.SyncfusionScheduler;
using MVCAgenda.Service.Appointments;
using MVCAgenda.Service.Helpers;
using MVCAgenda.Service.Logins;
using MVCAgenda.Service.Medics;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.Rooms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Scheduler
{
    public class SchedulerFactory : ISchedulerFactory
    {
        #region Fields

        private readonly IAppointmentService _appointmentServices;
        private readonly IPatientService _patientServices;
        private readonly IRoomService _roomServices;
        private readonly IMedicService _medicServices;
        private readonly ILoggerService _logger;
        private readonly IWorkContext _workContext;
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public SchedulerFactory(
            IAppointmentService appointmentServices,
            IPatientService patientServices,
            IRoomService roomServices,
            IMedicService medicServices,
            ILoggerService loggerServices,
            IWorkContext workContext,
            IDateTimeHelper dateTimeHelper)
        {
            _appointmentServices = appointmentServices;
            _patientServices = patientServices;
            _roomServices = roomServices;
            _medicServices = medicServices;
            _logger = loggerServices;
            _workContext = workContext;
            _dateTimeHelper = dateTimeHelper;
        }

        #endregion

        /***********************************************************************************/

        #region Methods

        public async Task<ScheduleEventData> PrepereScheduleItemListViewModel(Appointment appointment, Patient patient, Medic medic, Room room)
        {
            try
            {
                return new ScheduleEventData()
                {
                    Id = appointment.Id,
                    MedicId = appointment.MedicId,
                    RoomId = appointment.RoomId,
                    PatientId = appointment.PatientId,

                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    Mail = patient.Mail,

                    StartTime = await _dateTimeHelper.ConvertToUserTimeAsync(appointment.StartDate),
                    EndTime = await _dateTimeHelper.ConvertToUserTimeAsync(appointment.EndDate),

                    Medic = medic.Name,
                    Room = room.Name,
                    PrimaryColor = room.PrimaryColor,
                    SecondaryColor = room.SecondaryColor,

                    Subject = appointment.Procedure,
                    Description = appointment.Comments,

                    Made = appointment.Made,
                    AppointmentCreationDate = appointment.AppointmentCreationDate,
                    ResponsibleForAppointment = appointment.ResponsibleForAppointment
                };
            }
            catch
            {
                return new ScheduleEventData();
            }
        }

        public async Task<ScheduleList> GetAsync(
            DateTime? searchByAppointmentStartDate = null,
            DateTime? searchByAppointmentEndDate = null,
            string? mail = null)
        {
            try
            {
                var items = new List<ScheduleEventData>();

                List<Appointment> appointments = new List<Appointment>();

                //ToDo

                if (mail != null)
                {
                    var medic = await _medicServices.GetAsync(mail);
                    var medicId = medic.Id;
                    appointments = await _appointmentServices.GetFiltredListAsync(-1, searchByAppointmentStartDate: searchByAppointmentStartDate, searchByAppointmentEndDate: searchByAppointmentEndDate, searchByMedic: medicId, hidden: false);
                }
                else
                    appointments = await _appointmentServices.GetFiltredListAsync(-1, searchByAppointmentStartDate: searchByAppointmentStartDate, searchByAppointmentEndDate: searchByAppointmentEndDate, hidden: false);

                foreach (var appointment in appointments)
                    items.Add(await PrepereScheduleItemListViewModel(
                        appointment,
                        await _patientServices.GetAsync(appointment.PatientId),
                        await _medicServices.GetAsync(appointment.MedicId),
                        await _roomServices.GetAsync(appointment.RoomId)));

                return new ScheduleList() { AppointmentsSchedule = items };
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new ScheduleList();
            }
        }

        #endregion
    }
}
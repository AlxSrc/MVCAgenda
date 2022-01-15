using MVCAgenda.Core;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Enum;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Core.Logging;
using MVCAgenda.Factories.Scheduler;
using MVCAgenda.Models.SyncfusionScheduler;
using MVCAgenda.Service.Appointments;
using MVCAgenda.Service.Logins;
using MVCAgenda.Service.Medics;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.Rooms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Scheduler
{
    public class SchedulerManager : ISchedulerManager
    {
        #region Fields

        private readonly IAppointmentService _appointmentServices;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IPatientService _patientServices;
        private readonly IRoomService _roomServices;
        private readonly IMedicService _medicServices;
        private readonly ILoggerService _logger;
        private readonly IWorkContext _workContext;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public SchedulerManager(
            IAppointmentService appointmentServices,
            ISchedulerFactory schedulerFactory,
            IPatientService patientServices,
            IRoomService roomServices,
            IMedicService medicServices,
            ILoggerService loggerServices, 
            IWorkContext workContext)
        {
            _appointmentServices = appointmentServices;
            _schedulerFactory = schedulerFactory;
            _patientServices = patientServices;
            _roomServices = roomServices;
            _medicServices = medicServices;
            _logger = loggerServices;
            _workContext = workContext;
        }

        #endregion

        /**************************************************************************************/

        #region Methods

        public async Task<string> CreateAsync(ScheduleEventData scheduleData)
        {
            string User = "alexandru";
            try
            {
                int patientId;
                //string ressultSearch = await _appointmentServices.SearchAppointmentAsync(scheduleData.MedicId, scheduleData.RoomId, scheduleData.StartTime, scheduleData.EndTime);

                //if (ressultSearch != StringHelpers.SuccesMessage)
                //{
                //    return ressultSearch;
                //}

                if (scheduleData.PatientId > 0)
                {
                    var patient = await _patientServices.GetAsync(scheduleData.PatientId);
                    if (patient == null)
                    {
                        return "Error. Not found.";
                    }

                    patientId = patient.Id;
                }
                else
                {
                    var newPatient = new Patient()
                    {
                        FirstName = scheduleData.FirstName,
                        LastName = scheduleData.LastName,
                        PhoneNumber = scheduleData.PhoneNumber,
                        Mail = scheduleData.Mail
                    };

                    patientId = await _patientServices.CheckExistentPatientAsync(newPatient);
                }

                var newAppointment = new Appointment
                {
                    PatientId = patientId,
                    MedicId = scheduleData.MedicId,
                    RoomId = scheduleData.RoomId,
                    StartDate = scheduleData.StartTime,
                    EndDate = scheduleData.EndTime,
                    Procedure = scheduleData.Subject,
                    Made = true,
                    ResponsibleForAppointment = User,
                    AppointmentCreationDate = DateTime.Now,
                    Comments = scheduleData.Description,
                    AppointmentType = (int)AppointmentType.Insurance,
                    Hidden = false
                };

                var ressult = await _appointmentServices.CreateAsync(newAppointment);

                if (ressult == false)
                    return "Nu s-a putut creea programarea";
                else
                {
                    //var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Create}, Appointment: {newAppointment.Id}";
                    //await _logger.CreateAsync(msg, null, null, LogLevel.Error);

                    return StringHelpers.SuccesMessage;
                }
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Create}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return "Pacientul nu a putut fi adaugat, contacteaza administratorul.";
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
                    appointments = await _appointmentServices.GetFiltredListAsync(-1, searchByAppointmentStartDate: searchByAppointmentStartDate, searchByAppointmentEndDate:searchByAppointmentEndDate, hidden:false);

                foreach (var appointment in appointments)
                    items.Add(await _schedulerFactory.PrepereScheduleItemListViewModel(
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

        public async Task<string> UpdateAsync(ScheduleEventData scheduleData)
        {
            try
            {
                var appointment = new Appointment()
                {
                    Id = scheduleData.Id,
                    PatientId = scheduleData.PatientId,
                    MedicId = scheduleData.MedicId,
                    RoomId = scheduleData.RoomId,

                    Made = scheduleData.Made,
                    StartDate = scheduleData.StartTime,
                    EndDate = scheduleData.EndTime,
                    Procedure = scheduleData.Subject,
                    Comments = scheduleData.Description,

                    ResponsibleForAppointment = scheduleData.ResponsibleForAppointment,
                    AppointmentCreationDate = scheduleData.AppointmentCreationDate,
                    Hidden = false,
                };
                var test = await _appointmentServices.UpdateAsync(appointment);
                return StringHelpers.SuccesMessage;
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Appointments} Schedule manager, Action: {LogInfo.Edit}, Appointment: {scheduleData.Id}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return "Nu s-a putut modifica programarea";
            }
        }

        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                return StringHelpers.SuccesMessage;
            }
            catch (Exception Exception)
            {
                return "Programarea nu a putut fi stearsa.";
            }
        }

        public async Task<string> HideAsync(int id)
        {
            try
            {
                var asd = await _appointmentServices.HideAsync(id);
                return StringHelpers.SuccesMessage;
            }
            catch (Exception Exception)
            {
                return StringHelpers.MakeFailMessage(Exception.Message);
            }
        }

        #endregion
    }
}
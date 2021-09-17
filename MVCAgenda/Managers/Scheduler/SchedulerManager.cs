using MVCAgenda.Core.Domain;
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
        #endregion
        /***********************************************************************************/
        #region Constructor
        public SchedulerManager(
            IAppointmentService appointmentServices,
            ISchedulerFactory schedulerFactory,
            IPatientService patientServices,
            IRoomService roomServices,
            IMedicService medicServices,
            ILoggerService loggerServices)
        {
            _appointmentServices = appointmentServices;
            _schedulerFactory = schedulerFactory;
            _patientServices = patientServices;
            _roomServices = roomServices;
            _medicServices = medicServices;
            _logger = loggerServices;
        }
        #endregion
        /**************************************************************************************/
        #region Methods
        public async Task<string> CreateAsync(ScheduleEventData ScheduleData)
        {
            string User = "Administrator";

            try
            {
                int patientId;
                string date = ScheduleData.StartTime.ToString("yyyy-MM-dd");
                string hour = ScheduleData.StartTime.ToString("HH:mm");
                string message = await _appointmentServices.SearchAppointmentAsync(ScheduleData.MedicId, ScheduleData.RoomId, ScheduleData.StartTime, ScheduleData.EndTime);

                if (message != StringHelpers.SuccesMessage)
                {
                    return message;
                }

                if (ScheduleData.PatientId > 0)
                {
                    var patient = await _patientServices.GetAsync(ScheduleData.PatientId);
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
                        FirstName = ScheduleData.FirstName,
                        LastName = ScheduleData.LastName,
                        PhoneNumber = ScheduleData.PhoneNumber,
                        Mail = ScheduleData.Mail
                    };

                    patientId = await _patientServices.CheckExistentPatientAsync(newPatient);
                }

                var newAppointment = new Appointment
                {
                    PatientId = patientId,
                    MedicId = ScheduleData.MedicId,
                    RoomId = ScheduleData.RoomId,
                    StartDate = ScheduleData.StartTime,
                    EndDate = ScheduleData.EndTime,
                    Procedure = ScheduleData.Subject,
                    Made = true,
                    ResponsibleForAppointment = User,
                    AppointmentCreationDate = DateTime.Now,
                    Comments = ScheduleData.Description,
                    Hidden = false
                };

                var asd = await _appointmentServices.CreateAsync(newAppointment);

                await _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{User} created Appointment & Patient: Id Appointment:{ScheduleData.Id} {ScheduleData.FirstName}, {ScheduleData.PhoneNumber}",
                    FullMessage = null,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Information,
                    Hidden = false
                });

                return StringHelpers.SuccesMessage;
            }
            catch (Exception exception)
            {
                await _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{User} failed to add Appointment & Patient: Id Appointment:Appointment & Patient: Id Appointment:{ScheduleData.Id} {ScheduleData.FirstName}, {ScheduleData.PhoneNumber}",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
                return "Pacientul nu a putut fi adaugat, contacteaza administratorul.";
            }
        }

        public async Task<ScheduleList> GetAsync()
        {
            try
            {
                var items = new List<ScheduleEventData>();
                var appointments = await _appointmentServices.GetFiltredListAsync(DateTime.MinValue, DateTime.MinValue, 0,0,null,0,false,false);
                foreach (var appointment in appointments)
                    items.Add(await _schedulerFactory.PrepereScheduleItemListViewModel(
                        appointment, 
                        await _patientServices.GetAsync(appointment.PatientId), 
                        await _medicServices.GetAsync(appointment.MedicId), 
                        await _roomServices.GetAsync(appointment.RoomId)));

                return new ScheduleList() { AppointmentsSchedule = items };
            }
            catch (Exception Exception)
            {
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
            catch (Exception Exception)
            {
                return StringHelpers.MakeFailMessage(Exception.Message);
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
                return StringHelpers.MakeFailMessage(Exception.Message);
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

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
        private readonly IAppointmentServices _appointmentServices;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IPatientServices _patientServices;
        private readonly IRoomServices _roomServices;
        private readonly IMedicServices _medicServices;
        private readonly ILoggerServices _logger;
        #endregion
        /***********************************************************************************/
        #region Constructor
        public SchedulerManager(
            IAppointmentServices appointmentServices,
            ISchedulerFactory schedulerFactory,
            IPatientServices patientServices,
            IRoomServices roomServices,
            IMedicServices medicServices,
            ILoggerServices loggerServices)
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
                string message = await _appointmentServices.SearchAppointmentAsync(ScheduleData.MedicId, ScheduleData.RoomId, date, hour);

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
                        SecondName = ScheduleData.SecondName,
                        PhonNumber = ScheduleData.PhonNumber,
                        Mail = ScheduleData.Mail
                    };

                    patientId = await _patientServices.CheckExistentPatientAsync(newPatient);
                }

                var newAppointment = new Appointment
                {
                    PatientId = patientId,
                    MedicId = ScheduleData.MedicId,
                    RoomId = ScheduleData.RoomId,
                    AppointmentDate = date,
                    AppointmentHour = hour,
                    Procedure = ScheduleData.Subject,
                    Made = true,
                    ResponsibleForAppointment = User,
                    AppointmentCreationDate = DateTime.Now.ToString(),
                    Comments = ScheduleData.Description,
                    Hidden = false
                };

                await _appointmentServices.CreateAsync(newAppointment);

                await _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{User} created Appointment & Patient: Id Appointment:{ScheduleData.Id} {ScheduleData.FirstName}, {ScheduleData.PhonNumber}",
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
                    ShortMessage = $"{User} failed to add Appointment & Patient: Id Appointment:Appointment & Patient: Id Appointment:{ScheduleData.Id} {ScheduleData.FirstName}, {ScheduleData.PhonNumber}",
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
                var appointments = await _appointmentServices.GetListAsync(null,null,0,0,null,0,false,false);
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

        public async Task<string> UpdateAsync(ScheduleEventData ScheduleData)
        {
            try
            {
                var appointment = new Appointment()
                {
                    Id = ScheduleData.Id,
                    PatientId = ScheduleData.PatientId,
                    MedicId = ScheduleData.MedicId,
                    RoomId = ScheduleData.RoomId,

                    Made = ScheduleData.Made,
                    AppointmentDate = ScheduleData.StartTime.ToString("yyyy-MM-dd"),
                    AppointmentHour = ScheduleData.StartTime.ToString("HH:mm"),
                    Procedure = ScheduleData.Subject,
                    Comments = ScheduleData.Description,

                    ResponsibleForAppointment = ScheduleData.ResponsibleForAppointment,
                    AppointmentCreationDate = ScheduleData.AppointmentCreationDate,
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

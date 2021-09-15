using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Core.Logging;
using MVCAgenda.Factories.Appointments;
using MVCAgenda.Models.Appointments;
using MVCAgenda.Service.Appointments;
using MVCAgenda.Service.Logins;
using MVCAgenda.Service.Medics;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Appointments
{
    public class AppointmentsManager : IAppointmentsManager
    {
        string user = "admin"; 

        #region Fields
        private readonly IAppointmentServices _appointmentServices;
        private readonly IAppointmentsFactory _appointmentsFactory;
        private readonly IPatientServices _patientServices;
        private readonly IRoomServices _roomServices;
        private readonly IMedicServices _medicServices;
        private readonly ILoggerServices _logger;
        #endregion
        /***********************************************************************************/
        #region Constructor
        public AppointmentsManager(
            IAppointmentServices appointmentServices,
            IAppointmentsFactory appointmentsFactory,
            IPatientServices patientServices,
            IRoomServices roomServices,
            IMedicServices medicServices,
            ILoggerServices loggerServices)
        {
            _appointmentServices = appointmentServices;
            _appointmentsFactory = appointmentsFactory;
            _patientServices = patientServices;
            _roomServices = roomServices;
            _medicServices = medicServices;
            _logger = loggerServices;
        }
        #endregion
        /***********************************************************************************/
        #region Methods
        public async Task<string> CreateAsync(AppointmentCreateViewModel appointmentViewModel)
        {
            try
            {
                int patientId;
                string message = await _appointmentServices.SearchAppointmentAsync(appointmentViewModel.MedicId, appointmentViewModel.RoomId, appointmentViewModel.AppointmentDate, appointmentViewModel.AppointmentHour);

                if (message != StringHelpers.SuccesMessage)
                {
                    return message;
                }

                if (appointmentViewModel.PatientId > 0)
                {
                    var patient = await _patientServices.GetAsync(appointmentViewModel.PatientId);
                    if (patient == null)
                    {
                        return "Error. Not found.";
                    }
                    patientId = patient.Id;
                }
                else
                {
                    var newPatient = new Patient
                    {
                        FirstName = appointmentViewModel.FirstName,
                        SecondName = appointmentViewModel.SecondName,
                        PhonNumber = appointmentViewModel.PhonNumber,
                        Mail = appointmentViewModel.Mail
                    };

                    patientId = await _patientServices.CheckExistentPatientAsync(newPatient);
                }

                var newAppointment = new Appointment
                {
                    PatientId = patientId,
                    MedicId = appointmentViewModel.MedicId,
                    RoomId = appointmentViewModel.RoomId,
                    AppointmentDate = appointmentViewModel.AppointmentDate,
                    AppointmentHour = appointmentViewModel.AppointmentHour,
                    Procedure = appointmentViewModel.Procedure,
                    Made = true,
                    ResponsibleForAppointment = appointmentViewModel.ResponsibleForAppointment,
                    AppointmentCreationDate = DateTime.Now.ToString(),
                    Comments = appointmentViewModel.Comments,
                    Hidden = false
                };

                await _appointmentServices.CreateAsync(newAppointment);

                await CreateLog($"{user} created Appointment Id Appointment:{appointmentViewModel.AppointmentDate} {appointmentViewModel.Procedure}", null, LogLevel.Information);
                return StringHelpers.SuccesMessage;
            }
            catch (Exception exception)
            {
                await CreateLog($"{user} failed to add Appointment & Patient: Id Appointment:{appointmentViewModel} {appointmentViewModel.FirstName}, {appointmentViewModel.PhonNumber}", exception.Message, LogLevel.Error);
                return "Nu s-a putut adauga programarea.";
            }
        }
        
        public async Task<AppointmentsViewModel> GetListAsync(string SearchByName, string SearchByPhoneNumber, string SearchByEmail, string SearchByAppointmentHour, string SearchByAppointmentDate, int SearchByRoom, int SearchByMedic, string SearchByProcedure, int Id, bool daily, bool Hidden)
        {
            try
            {
                var appointmentsList = await _appointmentServices.GetListAsync(SearchByAppointmentHour, SearchByAppointmentDate, SearchByRoom, SearchByMedic, SearchByProcedure, Id, daily, Hidden);
                
                var appointmentsListViewModel = new List<AppointmentListItemViewModel>();
                foreach (var appointment in appointmentsList)
                    appointmentsListViewModel.Add(await _appointmentsFactory.PrepereAppointmentListItemViewModel(appointment, await _patientServices.GetAsync(appointment.PatientId), await _medicServices.GetAsync(appointment.MedicId), await _roomServices.GetAsync(appointment.RoomId)));

                var app = appointmentsListViewModel
                    .Where(p => !string.IsNullOrEmpty(SearchByName) ? p.FirstName.ToUpper().Contains(SearchByName.ToUpper()) : true)
                    .Where(p => !string.IsNullOrEmpty(SearchByPhoneNumber) ? p.PhonNumber.Contains(SearchByPhoneNumber) : true).ToList();

                return new AppointmentsViewModel()
                {
                    Blacklist = Hidden,
                    AppointmentsList = appointmentsListViewModel
                };
            }
            catch (Exception exception)
            {
                _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{user} failed to get appointments",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
                return null;
            }
        }
        
        public async Task<AppointmentDetailsViewModel> GetDetailsAsync(int id)
        {
            try
            {
                var appointment = await _appointmentServices.GetAsync(id);
                var patient = await _patientServices.GetAsync(appointment.PatientId);
                var medic = await _medicServices.GetAsync(appointment.MedicId);
                var room = await _roomServices.GetAsync(appointment.RoomId);

                return await _appointmentsFactory.PrepereAppointmentDetailsViewModel(appointment, patient, medic, room);
            }
            catch (Exception exception)
            {
                await CreateLog($"{user} failed to get appointment: id:{id}", exception.Message, LogLevel.Error);
                return null;
            }
        }
        public async Task<AppointmentEditViewModel> GetEditDetailsAsync(int id)
        {
            try
            {
                return await _appointmentsFactory.PrepereAppointmentEditDetailsViewModel(await _appointmentServices.GetAsync(id));
            }
            catch (Exception exception)
            {
                await CreateLog($"{user} failed to get appointment for edit: id:{id}", exception.Message, LogLevel.Error);
                return null;
            }
        }
        
        public async Task<string> UpdateAsync(AppointmentEditViewModel appointmentViewModel)
        {
            try
            {
                string message = await _appointmentServices.SearchAppointmentAsync(appointmentViewModel.MedicId, appointmentViewModel.RoomId, appointmentViewModel.AppointmentDate, appointmentViewModel.AppointmentHour);

                if (message != StringHelpers.SuccesMessage)
                {
                    return message;
                }

                var newAppointment = new Appointment
                {
                    Id = appointmentViewModel.Id,
                    PatientId = appointmentViewModel.PatientId,
                    MedicId = appointmentViewModel.MedicId,
                    RoomId = appointmentViewModel.RoomId,
                    AppointmentDate = appointmentViewModel.AppointmentDate,
                    AppointmentHour = appointmentViewModel.AppointmentHour,
                    Procedure = appointmentViewModel.Procedure,
                    Made = appointmentViewModel.Made,
                    ResponsibleForAppointment = appointmentViewModel.ResponsibleForAppointment,
                    AppointmentCreationDate = appointmentViewModel.AppointmentCreationDate,
                    Comments = appointmentViewModel.Comments,
                    Hidden = appointmentViewModel.Hidden
                };

                var test = await _appointmentServices.UpdateAsync(newAppointment);
                var est = await CreateLog($"{user} Updated Appointment: Id Appointment:{appointmentViewModel}", null, LogLevel.Information);
                return StringHelpers.SuccesMessage;
            }
            catch (Exception exception)
            {
                await CreateLog($"{user} failed to update Appointment: Id Appointment:{appointmentViewModel}", exception.Message, LogLevel.Error);
                return "Pacientul nu a putut fi adaugat, contacteaza administratorul.";
            }
        }
        
        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                if (await CheckExist(id) != true)
                {
                    return "Programarea nu a putut fi gasita.";
                    await CreateLog($"{user} failed to delete appointment: id:{id}, missing.", null, LogLevel.Error);
                }
                else
                {
                    await _appointmentServices.HideAsync(id);
                    await CreateLog($"{user} deleted appointment: id:{id}", null, LogLevel.Information);
                    return StringHelpers.SuccesMessage;
                }

            }
            catch (Exception exception)
            {
                await CreateLog($"{user} failed to delete appointment: id:{id}", exception.Message, LogLevel.Error);
                return "Programarea nu a putut fi stersa.";
            }
        }
        #endregion
        /***********************************************************************************/
        #region Utils
        private async Task<bool> CheckExist(int id)
        {
            var model = await _appointmentServices.GetAsync(id);

            if (model == null)
                return false;

            return true;
        }
        private async Task<bool> CreateLog(string message, string fullMessage, LogLevel logLevel)
        {
            await _logger.CreateAsync(new Log()
            {
                ShortMessage = message,
                FullMessage = fullMessage,
                CreatedOnUtc = DateTime.UtcNow,
                IpAddress = null,
                LogLevel = logLevel,
                Hidden = false
            });
            return true;
        }
        #endregion
    }
}

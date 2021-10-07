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
        private readonly IAppointmentService _appointmentServices;
        private readonly IAppointmentsFactory _appointmentsFactory;
        private readonly IPatientService _patientServices;
        private readonly IRoomService _roomServices;
        private readonly IMedicService _medicServices;
        private readonly ILoggerService _logger;
        #endregion
        /***********************************************************************************/
        #region Constructor
        public AppointmentsManager(
            IAppointmentService appointmentServices,
            IAppointmentsFactory appointmentsFactory,
            IPatientService patientServices,
            IRoomService roomServices,
            IMedicService medicServices,
            ILoggerService loggerServices)
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
                string message = await _appointmentServices.SearchAppointmentAsync(appointmentViewModel.MedicId, appointmentViewModel.RoomId, appointmentViewModel.StartDate, appointmentViewModel.EndDate);

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
                        LastName = appointmentViewModel.LastName,
                        PhoneNumber = appointmentViewModel.PhoneNumber,
                        Mail = appointmentViewModel.Mail
                    };

                    patientId = await _patientServices.CheckExistentPatientAsync(newPatient);
                }

                var newAppointment = new Appointment
                {
                    PatientId = patientId,
                    MedicId = appointmentViewModel.MedicId,
                    RoomId = appointmentViewModel.RoomId,
                    StartDate = appointmentViewModel.StartDate,
                    EndDate = appointmentViewModel.EndDate,
                    Procedure = appointmentViewModel.Procedure,
                    Made = true,
                    ResponsibleForAppointment = appointmentViewModel.ResponsibleForAppointment,
                    AppointmentCreationDate = DateTime.Now,
                    Comments = appointmentViewModel.Comments,
                    Hidden = false
                };

                var result = await _appointmentServices.CreateAsync(newAppointment);
                if (result == false)
                    return "Nu s-a putut creea programarea.";
                else
                {
                    var msg = $"User: {user}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Create}, Appointment: {newAppointment.Id}";
                    await _logger.CreateAsync(msg, null, null, LogLevel.Information);
                    return StringHelpers.SuccesMessage;
                }
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Create}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return "Nu s-a putut adauga programarea.";
            }
        }
        
        public async Task<AppointmentsViewModel> GetListAsync(string SearchByName = null,
            string SearchByPhoneNumber = null,
            string SearchByEmail = null,
            DateTime? SearchByAppointmentStartDate = null,
            DateTime? SearchByAppointmentEndDate = null,
            int? SearchByRoom = null,
            int? SearchByMedic = null,
            string SearchByProcedure = null,
            int? Id = null,
            bool? Daily = null,
            bool? Hidden = null)
        {
            try
            {
                var appointmentsList = await _appointmentServices.GetFiltredListAsync(SearchByAppointmentStartDate, SearchByAppointmentEndDate, SearchByRoom, SearchByMedic, SearchByProcedure, Id, Daily, Hidden);
                
                var appointmentsListViewModel = new List<AppointmentListItemViewModel>();
                foreach (var appointment in appointmentsList)
                    appointmentsListViewModel.Add(await _appointmentsFactory.PrepereAppointmentListItemViewModel(appointment, await _patientServices.GetAsync(appointment.PatientId), await _medicServices.GetAsync(appointment.MedicId), await _roomServices.GetAsync(appointment.RoomId)));

                var app = appointmentsListViewModel
                    .Where(p => !string.IsNullOrEmpty(SearchByName) ? p.FirstName.ToUpper().Contains(SearchByName.ToUpper()) : true)
                    .Where(p => !string.IsNullOrEmpty(SearchByPhoneNumber) ? p.PhoneNumber.Contains(SearchByPhoneNumber) : true).ToList();
                bool? Blacklist = null;
                return new AppointmentsViewModel()
                {
                    Hidden = Hidden == null ? false : Hidden,
                    Blacklist = Blacklist == null ? false : Blacklist,
                    AppointmentsList = appointmentsListViewModel
                };
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new AppointmentsViewModel();
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
                var msg = $"User: {user}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Read}, Appointment: {id}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
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
                var msg = $"User: {user}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Read}, Appointment: {id}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new AppointmentEditViewModel();
            }
        }
        
        public async Task<string> UpdateAsync(AppointmentEditViewModel appointmentViewModel)
        {
            try
            {
                string message = await _appointmentServices.SearchAppointmentAsync(appointmentViewModel.MedicId, appointmentViewModel.RoomId, appointmentViewModel.StartDate, appointmentViewModel.EndDate);

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
                    StartDate = appointmentViewModel.StartDate,
                    EndDate = appointmentViewModel.EndDate,
                    Procedure = appointmentViewModel.Procedure,
                    Made = appointmentViewModel.Made,
                    ResponsibleForAppointment = appointmentViewModel.ResponsibleForAppointment,
                    AppointmentCreationDate = appointmentViewModel.AppointmentCreationDate,
                    Comments = appointmentViewModel.Comments,
                    Hidden = appointmentViewModel.Hidden
                };

                var response = await _appointmentServices.UpdateAsync(newAppointment);
                if (response == false)
                {
                    return "Programarea nu a putut fi editata.";
                }
                else
                {
                    var msg = $"User: {user}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Edit}, Appointment: {appointmentViewModel.Id}";
                    await _logger.CreateAsync(msg, null, null, LogLevel.Information);
                    return StringHelpers.SuccesMessage;
                }
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Edit}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
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
                }
                else
                {
                    var response = await _appointmentServices.HideAsync(id);
                    if(response == false)
                    {
                        return "Programarea nu a putut fi stearsa.";
                    }
                    else
                    {
                        var msg = $"User: {user}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Hide}, Appointment: {id}";
                        await _logger.CreateAsync(msg, null, null, LogLevel.Information);
                        return StringHelpers.SuccesMessage;
                    }
                    
                }

            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Hide}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
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
        #endregion
    }
}

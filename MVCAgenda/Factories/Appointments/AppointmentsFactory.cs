using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Appointments;
using System.Threading.Tasks;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.Appointments;
using MVCAgenda.Service.Rooms;
using MVCAgenda.Service.Medics;
using MVCAgenda.Service.Logins;
using System;
using MVCAgenda.Core.Logging;
using System.Collections.Generic;
using System.Linq;
using MVCAgenda.Core.Enum;
using MVCAgenda.Core;
using MVCAgenda.Core.Pagination;
using MVCAgenda.Service.Helpers;

namespace MVCAgenda.Factories.Appointments
{
    public class AppointmentsFactory : IAppointmentsFactory
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

        public AppointmentsFactory(
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

        public async Task<AppointmentsViewModel> PrepereAppointmentsListAsync(int pageIndex,
            string searchByName = null,
            string searchByPhoneNumber = null,
            string searchByEmail = null,
            DateTime? searchByAppointmentStartDate = null,
            DateTime? searchByAppointmentEndDate = null,
            int? searchByRoom = null,
            int? searchByMedic = null,
            string searchByProcedure = null,
            int? id = null,
            bool? made = null,
            bool? daily = null,
            bool? hidden = null)
        {
            try
            {
                bool? Blacklist = null;
                var appointments = await _appointmentServices.GetAppointmentsPaginationAsync(pageIndex, searchByName, searchByPhoneNumber, searchByEmail, searchByAppointmentStartDate, searchByAppointmentEndDate, searchByRoom, searchByMedic, searchByProcedure, id, made, daily, hidden);

                var appointmentsListViewModel = new List<AppointmentListItemViewModel>();
                foreach (var appointment in appointments.Appointments)
                    appointmentsListViewModel.Add(await PrepereAppointmentListItemAsync(appointment));
                var totalPages = appointments.TotalPages;

                return new AppointmentsViewModel()
                {
                    PageIndex = pageIndex,
                    TotalPages = totalPages,
                    Made = made,
                    Daily = daily,
                    SearchByAppointmentStartDate = searchByAppointmentStartDate,
                    SearchByAppointmentEndDate = searchByAppointmentEndDate,
                    SearchByMedic = searchByMedic,
                    SearchByRoom = searchByRoom,
                    SearchByProcedure = searchByProcedure,
                    SearchByPhoneNumber = searchByPhoneNumber,
                    SearchByEmail = searchByEmail,
                    SearchByName = searchByName,
                    Id = id,
                    Hidden = hidden,
                    Blacklist = Blacklist,

                    AppointmentsList = appointmentsListViewModel
                };
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new AppointmentsViewModel();
            }
        }

        public async Task<AppointmentDetailsViewModel> PrepereDetailsViewModelAsync(int id)
        {
            try
            {
                var appointment = await _appointmentServices.GetAsync(id);
                var patient = await _patientServices.GetAsync(appointment.PatientId);
                var medic = await _medicServices.GetAsync(appointment.MedicId);
                var room = await _roomServices.GetAsync(appointment.RoomId);
                var viewModel = new AppointmentDetailsViewModel()
                {
                    Id = appointment.Id,
                    PatientId = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    Mail = patient.Mail,
                    Medic = medic.Name,
                    Room = room.Name,
                    StartDate = (await _dateTimeHelper.ConvertToUserTimeAsync(appointment.StartDate)).ToString("dd/MMMM/yy HH:mm"),
                    EndDate = (await _dateTimeHelper.ConvertToUserTimeAsync(appointment.EndDate)).ToString("dd/MMMM/yy HH:mm"),
                    Procedure = appointment.Procedure,
                    ResponsibleForAppointment = appointment.ResponsibleForAppointment,
                    AppointmentCreationDate = (await _dateTimeHelper.ConvertToUserTimeAsync(appointment.AppointmentCreationDate)).ToString("dd/MMMM/yy HH:mm"),
                    Comments = appointment.Comments,
                    Hidden = appointment.Hidden
                };

                if (appointment.Made == true)
                {
                    viewModel.MadeText = "<span class=\"badge bg-success\">Da</span>";
                }
                else
                {
                    viewModel.MadeText = "<span class=\"badge bg-danger\">Nu</span>";
                }

                if (appointment.AppointmentType == (int)AppointmentType.Private)
                    viewModel.AppointmentType = $"<span>Programare privată</span>";
                else if (appointment.AppointmentType == (int)AppointmentType.Insurance)
                    viewModel.AppointmentType = $"<span>Programare prin casa de asigurări</span>";

                if (patient.StatusCode == (int)PatientStatus.Blacklist)
                {
                    viewModel.Blacklist = "<span class=\"badge bg-Danger\">Lista neagra</span>";
                }
                else if (patient.StatusCode == (int)PatientStatus.LoyalPatient)
                {
                    viewModel.Blacklist = "<span class=\"badge bg-primary\">Pacient loial</span>";
                }
                else
                {
                    viewModel.Blacklist = "<span class=\"badge bg-success\">Pacient</span>";
                }

                return viewModel;
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Read}, Appointment: {id}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return null;
            }
        }

        public async Task<AppointmentEditViewModel> PrepereEditViewModelAsync(int id)
        {
            try
            {
                var appointment = await _appointmentServices.GetAsync(id);
                var patient = await _patientServices.GetAsync(appointment.PatientId);
                var viewModel = new AppointmentEditViewModel()
                {
                    Id = appointment.Id,
                    PatientId = appointment.PatientId,
                    PatientName = $"{patient.FirstName.ToUpper()} {patient.LastName}",
                    MedicId = appointment.MedicId,
                    RoomId = appointment.RoomId,
                    Made = appointment.Made,
                    StartDate = await _dateTimeHelper.ConvertToUserTimeAsync(appointment.StartDate),
                    EndDate = await _dateTimeHelper.ConvertToUserTimeAsync(appointment.EndDate),
                    Procedure = appointment.Procedure,
                    PrivateAppointment = appointment.AppointmentType == (int)AppointmentType.Private ? true : false,
                    ResponsibleForAppointment = appointment.ResponsibleForAppointment,
                    AppointmentCreationDate = appointment.AppointmentCreationDate,
                    Comments = appointment.Comments,
                    Hidden = appointment.Hidden
                };

                return viewModel;
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Read}, Appointment: {id}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new AppointmentEditViewModel();
            }
        }

        #endregion

        /***********************************************************************************/

        #region Utils

        private async Task<AppointmentListItemViewModel> PrepereAppointmentListItemAsync(AppointmentListItem appointment)
        {
            var viewModel = new AppointmentListItemViewModel()
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                FirstName = appointment.FirstName,
                PhoneNumber = appointment.PhoneNumber,
                Mail = appointment.Mail,
                Medic = appointment.Medic,
                Room = appointment.Room,
                StartDate = (await _dateTimeHelper.ConvertToUserTimeAsync(appointment.StartDate)).ToString("dd-MMMM HH:mm"),
                EndDate = (await _dateTimeHelper.ConvertToUserTimeAsync(appointment.EndDate)).ToString("dd-MMMM HH:mm"),
                Procedure = appointment.Procedure,
                Hidden = appointment.Hidden
            };

            if (appointment.AppointmentType == (int)AppointmentType.Private)
                viewModel.Procedure = $"<span class=\"appointments-private\">{appointment.Procedure}</span>";
            else if (appointment.AppointmentType == (int)AppointmentType.Insurance)
                viewModel.Procedure = $"<span class=\"appointments-insurance\">{appointment.Procedure}</span>";

            return viewModel;
        }

        #endregion
    }
}
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

namespace MVCAgenda.Factories.Appointments
{
    public class AppointmentsFactory : IAppointmentsFactory
    {
        string user = "admin";

        #region Fields

        private readonly IAppointmentService _appointmentServices;
        private readonly IPatientService _patientServices;
        private readonly IRoomService _roomServices;
        private readonly IMedicService _medicServices;
        private readonly ILoggerService _logger;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public AppointmentsFactory(
            IAppointmentService appointmentServices,
            IPatientService patientServices,
            IRoomService roomServices,
            IMedicService medicServices,
            ILoggerService loggerServices)
        {
            _appointmentServices = appointmentServices;
            _patientServices = patientServices;
            _roomServices = roomServices;
            _medicServices = medicServices;
            _logger = loggerServices;
        }

        #endregion

        /***********************************************************************************/

        #region Methods

        public async Task<AppointmentsViewModel> PrepereListViewModelAsync(string SearchByName = null,
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
                    appointmentsListViewModel.Add(PrepereAppointmentListItem(appointment, await _patientServices.GetAsync(appointment.PatientId), await _medicServices.GetAsync(appointment.MedicId), await _roomServices.GetAsync(appointment.RoomId)));

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

        public async Task<AppointmentDetailsViewModel> PrepereDetailsViewModelAsync(int id)
        {
            try
            {
                var appointment = await _appointmentServices.GetAsync(id);
                var patient = await _patientServices.GetAsync(appointment.PatientId);
                var medic = await _medicServices.GetAsync(appointment.MedicId);
                var room = await _roomServices.GetAsync(appointment.RoomId);

                return PrepereAppointment(appointment, patient, medic, room);
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Read}, Appointment: {id}";
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
                return PrepereAppointmentEdit(appointment, patient);
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Appointments} manager, Action: {LogInfo.Read}, Appointment: {id}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new AppointmentEditViewModel();
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

        static AppointmentListItemViewModel PrepereAppointmentListItem(Appointment model, Patient patient, Medic medic, Room room)
        {
            var viewModel = new AppointmentListItemViewModel()
            {
                Id = model.Id,
                PatientId = model.PatientId,
                FirstName = patient.FirstName,
                PhoneNumber = patient.PhoneNumber,
                Medic = medic.Name,
                Room = room.Name,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Procedure = model.Procedure,
                Hidden = model.Hidden
            };

            if (model.Made == true)
                viewModel.Procedure = $"<span class=\"text-success\">{model.Procedure}</span>";
            else
                viewModel.Procedure = $"<span class=\"text-danger\">{model.Procedure}</span>";

            return viewModel;
        }

        static AppointmentDetailsViewModel PrepereAppointment(Appointment model, Patient patient, Medic medic, Room room)
        {
            var viewModel = new AppointmentDetailsViewModel()
            {
                Id = model.Id,
                PatientId = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                Mail = patient.Mail,
                Medic = medic.Name,
                Room = room.Name,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                Procedure = model.Procedure,
                ResponsibleForAppointment = model.ResponsibleForAppointment,
                AppointmentCreationDate = model.AppointmentCreationDate,
                Comments = model.Comments,
                Hidden = model.Hidden
            };

            if (model.Made == true)
            {
                viewModel.MadeText = "<span class=\"badge bg-success\">Da</span>";
            }
            else
            {
                viewModel.MadeText = "<span class=\"badge bg-danger\">Nu</span>";
            }

            if (patient.Blacklist == true)
            {
                viewModel.Blacklist = "<span class=\"badge bg-Danger\">Da</span>";
            }
            else if (patient.Blacklist == false)
            {
                viewModel.Blacklist = "<span class=\"badge bg-success\">Nu</span>";
            }

            return viewModel;
        }

        static AppointmentEditViewModel PrepereAppointmentEdit(Appointment appointment, Patient patient)
        {
            var viewModel = new AppointmentEditViewModel()
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                PatientName = $"{patient.FirstName.ToUpper()} {patient.LastName}",
                MedicId = appointment.MedicId,
                RoomId = appointment.RoomId,
                Made = appointment.Made,
                EndDate = appointment.EndDate,
                StartDate = appointment.StartDate,
                Procedure = appointment.Procedure,
                ResponsibleForAppointment = appointment.ResponsibleForAppointment,
                AppointmentCreationDate = appointment.AppointmentCreationDate,
                Comments = appointment.Comments,
                Hidden = appointment.Hidden
            };

            return viewModel;
        }

        #endregion
    }
}
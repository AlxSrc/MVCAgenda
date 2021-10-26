﻿using MVCAgenda.Core.Domain;
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
using MVCAgenda.Core.Status;
using MVCAgenda.Core.Helpers;

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

        public async Task<AppointmentsViewModel> PrepereListViewModelAsync(int pageIndex,
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
                var appointmentsList = await _appointmentServices.GetFiltredListAsync(pageIndex, searchByAppointmentStartDate, searchByAppointmentEndDate, searchByRoom, searchByMedic, searchByProcedure, id, made, daily, hidden);

                var totalAppointments = await _appointmentServices.GetNumberOfFiltredAppointmentsAsync(searchByAppointmentStartDate, searchByAppointmentEndDate, searchByRoom, searchByMedic, searchByProcedure, id, made, daily, hidden);
                var totalPages = (int)Math.Ceiling(totalAppointments / (double)Constants.TotalItemsOnAPage);

                var appointmentsListViewModel = new List<AppointmentListItemViewModel>();
                foreach (var appointment in appointmentsList)
                    appointmentsListViewModel.Add(await PrepereAppointmentListItem(appointment));

                var app = appointmentsListViewModel
                    .Where(p => !string.IsNullOrEmpty(searchByName) ? p.FirstName.ToUpper().Contains(searchByName.ToUpper()) : true)
                    .Where(p => !string.IsNullOrEmpty(searchByPhoneNumber) ? p.PhoneNumber.Contains(searchByPhoneNumber) : true)
                    .Where(p => !string.IsNullOrEmpty(searchByEmail) ? p.Mail.Contains(searchByEmail) : true).ToList();

                bool? Blacklist = null;
                return new AppointmentsViewModel()
                {
                    PageIndex = pageIndex,
                    TotalPages = totalPages,
                    Made = made,
                    Hidden = hidden == null ? false : hidden,
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
                    EndDate = appointment.EndDate,
                    StartDate = appointment.StartDate,
                    Procedure = appointment.Procedure,
                    ResponsibleForAppointment = appointment.ResponsibleForAppointment,
                    AppointmentCreationDate = appointment.AppointmentCreationDate,
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

                if (patient.StatusCode == (int)PatientStatus.Blacklist)
                {
                    viewModel.Blacklist = "<span class=\"badge bg-Danger\">Lista neagra</span>";
                }
                else if (patient.StatusCode == (int)PatientStatus.Patient)
                {
                    viewModel.Blacklist = "<span class=\"badge bg-success\">Pacient loial</span>";
                }
                else
                {
                    viewModel.Blacklist = "<span class=\"badge bg-success\">Pacient</span>";
                }

                return viewModel;
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

        private async Task<AppointmentListItemViewModel> PrepereAppointmentListItem(Appointment appointment)
        {
            var patient = await _patientServices.GetAsync(appointment.PatientId);
            var medic = await _medicServices.GetAsync(appointment.MedicId);
            var room = await _roomServices.GetAsync(appointment.RoomId);
            var viewModel = new AppointmentListItemViewModel()
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                FirstName = patient.FirstName,
                PhoneNumber = patient.PhoneNumber,
                Mail = patient.Mail,
                Medic = medic.Name,
                Room = room.Name,
                StartDate = appointment.StartDate,
                EndDate = appointment.EndDate,
                Procedure = appointment.Procedure,
                Hidden = appointment.Hidden
            };

            if (appointment.Made == true)
                viewModel.Procedure = $"<span class=\"text-success\">{appointment.Procedure}</span>";
            else
                viewModel.Procedure = $"<span class=\"text-danger\">{appointment.Procedure}</span>";

            return viewModel;
        }

        #endregion
    }
}
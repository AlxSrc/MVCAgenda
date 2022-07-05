using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCAgenda.Core;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Core.Logging;
using MVCAgenda.Core.Enum;
using MVCAgenda.Factories.Patients;
using MVCAgenda.Models.Patients;
using MVCAgenda.Service.Logins;
using MVCAgenda.Service.Patients;

namespace MVCAgenda.Managers.Patients
{
    public class PatientsManager : IPatientsManager
    {
        #region Fields

        private readonly IPatientService _patientServices;
        private readonly ILoggerService _logger;
        private readonly IWorkContext _workContext;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public PatientsManager(IPatientService patientServices,
            ILoggerService loggerServices,
            IWorkContext workContext)
        {
            _patientServices = patientServices;
            _logger = loggerServices;
            _workContext = workContext;
        }

        #endregion

        /***********************************************************************************/

        #region Methods

        public async Task<string> CreateAsync(PatientViewModel patientViewModel)
        {
            try
            {
                string msg;

                var patients = await _patientServices.GetListAsync(-1,searchByPhoneNumber: patientViewModel.PhoneNumber);

                if (patients.Count >= 1)
                {
                    msg = $"Exista un pacient cu numele: {patientViewModel.FirstName}, numarul de telefon: {patientViewModel.PhoneNumber}";
                    return msg;
                }
                else
                {
                    var patient = new Patient()
                    {
                        FirstName = $"{patientViewModel.FirstName.Substring(0, 1).ToUpper()}{patientViewModel.FirstName.Substring(1, patientViewModel.FirstName.Length - 1).ToLower()}",
                        LastName = patientViewModel.LastName != null ? $"{patientViewModel.LastName.Substring(0, 1).ToUpper()}{patientViewModel.LastName.Substring(1, patientViewModel.LastName.Length - 1).ToLower()}" : null,
                        PhoneNumber = patientViewModel.PhoneNumber,
                        Mail = patientViewModel.Mail,
                        StatusCode = (int)PatientStatus.Patient,
                        Hidden = false
                    };

                    var result = await _patientServices.CreateAsync(patient);
                    if (result == -1)
                        return "Pacientul nu a putut fi adaugat.";
                    else
                    {
                        //msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients} manager, Action: {LogInfo.Create}, Patient: {patient.Id}";
                        //await _logger.CreateAsync(msg, null, null, LogLevel.Information);

                        return StringHelpers.SuccesMessage;
                    }
                }
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return "Pacientul nu a putut fi adaugat.";
            }
        }

        public async Task<string> UpdateAsync(PatientViewModel patientViewModel)
        {
            try
            {
                if (await CheckExist(patientViewModel.Id) != true)
                {
                    return "Pacientul nu a putut fi gasit.";
                }
                else
                {
                    Patient patient = new Patient()
                    {
                        Id = patientViewModel.Id,
                        FirstName = $"{patientViewModel.FirstName.Substring(0, 1).ToUpper()}{patientViewModel.FirstName.Substring(1, patientViewModel.FirstName.Length - 1).ToLower()}",
                        LastName = patientViewModel.LastName != null ? $"{patientViewModel.LastName.Substring(0, 1).ToUpper()}{patientViewModel.LastName.Substring(1, patientViewModel.LastName.Length - 1).ToLower()}" : null,
                        PhoneNumber = patientViewModel.PhoneNumber,
                        Mail = patientViewModel.Mail,
                        StatusCode = patientViewModel.StatusCode,
                        Hidden = patientViewModel.Hidden
                    };

                    var result = await _patientServices.UpdateAsync(patient);
                    if (result == false)
                        return "Pacientul nu a putut fi editat.";
                    else
                    {
                        //var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients} manager, Action: {LogInfo.Edit}, Patient: {patient.Id}";
                        //await _logger.CreateAsync(msg, null, null, LogLevel.Information);

                        return StringHelpers.SuccesMessage;
                    }
                }
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients} manager, Action: {LogInfo.Edit}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return "Pacientul nu a putut fi editat.";
            }
        }

        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                if (await CheckExist(id) == false)
                {
                    return "Pacientul nu a putut fi gasit.";
                }
                else
                {
                    var result = await _patientServices.HideAsync(id);
                    if (result == false)
                        return "Pacientul nu a putut fi sters.";
                    else
                    {
                        //var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients} manager, Action: {LogInfo.Delete}, Patient: {id}";
                        //await _logger.CreateAsync(msg, null, null, LogLevel.Information);

                        return StringHelpers.SuccesMessage;
                    }
                }
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients} manager, Action: {LogInfo.Delete}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return "Pacientul nu a putut fi sters.";
            }
        }

        #endregion

        /***********************************************************************************/

        #region Utils

        private async Task<bool> CheckExist(int id)
        {
            var model = await _patientServices.GetAsync(id);

            if (model == null)
                return false;

            return true;
        }

        #endregion
    }
}
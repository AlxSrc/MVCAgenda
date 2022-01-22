using MVCAgenda.Core;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Core.Logging;
using MVCAgenda.Factories.Consultations;
using MVCAgenda.Models.Consultations;
using MVCAgenda.Service.Consultations;
using MVCAgenda.Service.Helpers;
using MVCAgenda.Service.Logins;
using System;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Consultations
{
    public class ConsultationsManager : IConsultationsManager
    {
        #region Fields

        private readonly IConsultationService _consultationServices;
        private readonly IConsultationsFactory _consultationFactory;
        private readonly ILoggerService _logger;
        private readonly IWorkContext _workContext;
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public ConsultationsManager(IConsultationService consultationServices, 
            IConsultationsFactory consultationFactory, 
            ILoggerService loggerServices,
            IWorkContext workContext,
            IDateTimeHelper dateTimeHelper)
        {
            _consultationServices = consultationServices;
            _consultationFactory = consultationFactory;
            _logger = loggerServices;
            _workContext = workContext;
            _dateTimeHelper = dateTimeHelper;
        }

        #endregion

        /***********************************************************************************/

        #region Methods

        public async Task<string> CreateAsync(ConsultationCreateViewModel consultationViewModel)
        {
            try
            {
                var consultation = new Consultation()
                {
                    PatientSheetId = consultationViewModel.PatientSheetId,
                    Diagnostic = consultationViewModel.Diagnostic,
                    Prescriptions = consultationViewModel.Prescriptions,
                    Symptoms = consultationViewModel.Symptoms,
                    CreationDate = _dateTimeHelper.ConvertToUtcTime(DateTime.Now),
                    Hidden = false
                };

                var result = await _consultationServices.CreateAsync(consultation);
                if (result == false)
                    return "Consultatia nu a putut fi adaugata.";
                else
                {
                    //var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Consultations} manager, Action: {LogInfo.Create}, Consultation: {consultation.Id}";
                    //await _logger.CreateAsync(msg, null, null, LogLevel.Information);

                    return StringHelpers.SuccesMessage;
                }
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Consultations} manager, Action: {LogInfo.Create}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Information);
                return "Consultatia nu a putut fi adaugata.";
            }
        }

        public async Task<string> UpdateAsync(ConsultationEditViewModel consultationViewModel)
        {
            try
            {
                if (await CheckExist(consultationViewModel.Id) == false)
                {
                    return "Consultatia nu a putut fi gasita.";
                }
                else
                {
                    var consultation = new Consultation()
                    {
                        Id = consultationViewModel.Id,
                        PatientSheetId = consultationViewModel.PatientSheetId,
                        Diagnostic = consultationViewModel.Diagnostic,
                        Prescriptions = consultationViewModel.Prescriptions,
                        Symptoms = consultationViewModel.Symptoms,
                        CreationDate = _dateTimeHelper.ConvertToUtcTime(consultationViewModel.CreationDate),
                        Hidden = consultationViewModel.Hidden
                    };

                    var result = await _consultationServices.UpdateAsync(consultation);
                    if (result == false)
                        return "Consultatia nu a putut fi editata.";
                    else
                    {
                        //var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Consultations} manager, Action: {LogInfo.Edit}, Consultation: {consultationViewModel.Id}";
                        //await _logger.CreateAsync(msg, null, null, LogLevel.Information);

                        return StringHelpers.SuccesMessage;
                    }
                }
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Consultations} manager, Action: {LogInfo.Edit}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return "Consultatia nu a putut fi editata.";
            }
        }

        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                if (await CheckExist(id) == false)
                {
                    return "Consultatia nu a putut fi gasita.";
                }
                else
                {
                    var result = await _consultationServices.HideAsync(id);
                    if (result == false)
                        return "Consultatia nu a putut fi stearsa.";
                    else
                    {
                        //var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Consultations} manager, Action: {LogInfo.Delete}, Consultation: {id}";
                        //await _logger.CreateAsync(msg, null, null, LogLevel.Information);

                        return StringHelpers.SuccesMessage;
                    }
                }
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Consultations} manager, Action: {LogInfo.Delete}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return "Consultatia nu a putut fi stearsa.";
            }
        }

        #endregion

        /***********************************************************************************/

        #region Utils

        private async Task<bool> CheckExist(int id)
        {
            var model = await _consultationServices.GetAsync(id);

            if (model == null)
                return false;

            return true;
        }

        #endregion
    }
}
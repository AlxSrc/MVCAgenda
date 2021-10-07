using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Core.Logging;
using MVCAgenda.Factories.Consultations;
using MVCAgenda.Factories.PatientsSheet;
using MVCAgenda.Models.Consultations;
using MVCAgenda.Models.PatientSheets;
using MVCAgenda.Service.Consultations;
using MVCAgenda.Service.Logins;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.PatientsSheet;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.PatientsSheets
{
    public class PatientsSheetsManager : IPatientsSheetsManager
    {
        private string user = "admin";

        #region Fields
        private readonly IPatientsSheetsFactory _patientSheetFactory;
        private readonly IPatientSheetService _patientSheetServices;
        private readonly IPatientService _patientServices;
        private readonly IConsultationService _consultationServices;
        private readonly IConsultationsFactory _consultationFactory;
        private readonly ILoggerService _logger;
        #endregion
        /***********************************************************************************/
        #region Constructor
        public PatientsSheetsManager(
            IPatientSheetService patientSheetServices,
            IPatientsSheetsFactory patientSheetFactory,
            IPatientService patientServices, 
            IConsultationService consultationServices, 
            IConsultationsFactory consultationFactory, 
            ILoggerService loggerServices)
        {
            _patientSheetServices = patientSheetServices;
            _patientSheetFactory = patientSheetFactory;
            _patientServices = patientServices;
            _consultationServices = consultationServices;
            _consultationFactory = consultationFactory;
            _logger = loggerServices;
        }
        #endregion
        /***********************************************************************************/
        #region Methods
        public async Task<PatientSheetDetailsViewModel> GetDetailsAsync(int id)
        {
            try
            {
                var consultations = await _consultationServices.GetListAsync(id);
                var consultationsList = new List<ConsultationViewModel>();
                foreach (var consultation in consultations)
                    consultationsList.Add(await _consultationFactory.PrepereConsultationViewModel(consultation));

                var patient = await _patientServices.GetAsync(id, true);

                return _patientSheetFactory.PreperePatientSheetDetailsViewModel(await _patientSheetServices.GetAsync(id), patient, consultationsList);
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.PatientSheets} manager, Action: {LogInfo.Read}, PatientSheets: {id}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new PatientSheetDetailsViewModel();
            }
        }

        public async Task<PatientSheetEditViewModel> GetEditDetailsAsync(int id)
        {
            try
            {
                return _patientSheetFactory.PreperePatientSheetEditViewModel(await _patientSheetServices.GetAsync(id));
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.PatientSheets} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new PatientSheetEditViewModel();
            }
        }

        public async Task<string> UpdateAsync(PatientSheetEditViewModel model)
        {
            try
            {
                if (await CheckExist(model.Id) == false)
                {
                    return "Fisa pacientului nu a putut fi gasita.";
                }
                else
                {
                    var patientSheet = new PatientSheet()
                    {
                        Id = model.Id,
                        AntecedentsH = model.AntecedentsH,
                        AntecedentsP = model.AntecedentsP,
                        NationalIdentificationNumber = model.NationalIdentificationNumber,
                        DateOfBirth = model.DateOfBirth,
                        Gender = Int32.Parse(model.Gender),
                        PhysicalExamination = model.PhysicalExamination,
                        Street = model.Street,
                        Town = model.Town,
                        Hidden = model.Hidden
                    };

                    var result = await _patientSheetServices.UpdateAsync(patientSheet);
                    if(result == false)
                        return "Fisa pacientului nu a putut fi editata.";
                    else
                    {
                        var msg = $"User: {user}, Table:{LogTable.PatientSheets} manager, Action: {LogInfo.Edit}, Appointment: {model.Id}";
                        await _logger.CreateAsync(msg, null, null, LogLevel.Information);
                        return StringHelpers.SuccesMessage;
                    }
                }

            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.PatientSheets} manager, Action: {LogInfo.Edit}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return "Fisa pacientului nu a putut fi editata.";
            }

        }
        #endregion
        /***********************************************************************************/
        #region Utils
        private async Task<bool> CheckExist(int id)
        {
            var model = await _patientSheetServices.GetAsync(id);

            if (model == null)
                return false;

            return true;
        }
        #endregion
    }
}

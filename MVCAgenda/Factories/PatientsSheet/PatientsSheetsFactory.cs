using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Consultations;
using MVCAgenda.Models.PatientSheets;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.PatientsSheet;
using MVCAgenda.Service.Consultations;
using MVCAgenda.Factories.Consultations;
using MVCAgenda.Service.Logins;
using System;
using MVCAgenda.Core.Logging;
using MVCAgenda.Core.Helpers;

namespace MVCAgenda.Factories.PatientsSheet
{
    public class PatientsSheetsFactory : IPatientsSheetsFactory
    {
        private string user = "admin";

        #region Fields

        private readonly IPatientSheetService _patientSheetServices;
        private readonly IPatientService _patientServices;
        private readonly IConsultationService _consultationServices;
        private readonly IConsultationsFactory _consultationFactory;
        private readonly ILoggerService _logger;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public PatientsSheetsFactory(
            IPatientSheetService patientSheetServices,
            IPatientService patientServices,
            IConsultationService consultationServices,
            IConsultationsFactory consultationFactory,
            ILoggerService loggerServices)
        {
            _patientSheetServices = patientSheetServices;
            _patientServices = patientServices;
            _consultationServices = consultationServices;
            _consultationFactory = consultationFactory;
            _logger = loggerServices;
        }

        #endregion

        /***********************************************************************************/

        #region Methods

        public async Task<PatientSheetDetailsViewModel> PrepereDetailsViewModelAsync(int id)
        {
            try
            {
                var consultations = await _consultationServices.GetListAsync(id);
                var consultationsList = new List<ConsultationViewModel>();
                foreach (var consultation in consultations)
                    consultationsList.Add(await _consultationFactory.PrepereViewModelAsync(consultation));

                var patient = await _patientServices.GetAsync(id);
                var patientSheets = await _patientSheetServices.GetAsync(id);

                return PrepereDetails(patientSheets, patient, consultationsList);
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.PatientSheets} manager, Action: {LogInfo.Read}, PatientSheets: {id}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new PatientSheetDetailsViewModel();
            }
        }

        public async Task<PatientSheetEditViewModel> PrepereEditViewModelAsync(int id)
        {
            try
            {
                var patientSheet = await _patientSheetServices.GetAsync(id);
                return new PatientSheetEditViewModel()
                {
                    Id = patientSheet.Id,
                    AntecedentsH = patientSheet.AntecedentsH,
                    AntecedentsP = patientSheet.AntecedentsP,
                    PhysicalExamination = patientSheet.PhysicalExamination,
                    NationalIdentificationNumber = patientSheet.NationalIdentificationNumber,
                    DateOfBirth = patientSheet.DateOfBirth,
                    Town = patientSheet.Town,
                    Street = patientSheet.Street,
                    Hidden = patientSheet.Hidden
                };
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.PatientSheets} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new PatientSheetEditViewModel();
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

        public PatientSheetDetailsViewModel PrepereDetails(PatientSheet patientSheet, Patient patient, List
            <ConsultationViewModel> consultations)
        {
            PatientSheetDetailsViewModel preparedView = new PatientSheetDetailsViewModel()
            {
                Id = patientSheet.Id,
                PatientId = patient.Id,
                FirstName = patient.FirstName,
                SecondName = patient.LastName,
                AntecedentsH = patientSheet.AntecedentsH,
                AntecedentsP = patientSheet.AntecedentsP,
                PhysicalExamination = patientSheet.PhysicalExamination,
                NationalIdentificationNumber = patientSheet.NationalIdentificationNumber,
                DateOfBirth = patientSheet.DateOfBirth,
                Town = patientSheet.Town,
                Street = patientSheet.Street,
                Consultations = consultations,
                Hidden = patientSheet.Hidden
            };

            if (patientSheet.Gender == 1)
            {
                preparedView.Gender = "Masculin";
            }
            else if (patientSheet.Gender == 0)
            {
                preparedView.Gender = "Feminin";
            }
            else
            {
                preparedView.Gender = "-";
            }

            return preparedView;
        }

        #endregion
    }
}
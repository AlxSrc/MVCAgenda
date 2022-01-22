using MVCAgenda.Core;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Logging;
using MVCAgenda.Models.Consultations;
using MVCAgenda.Service.Consultations;
using MVCAgenda.Service.Helpers;
using MVCAgenda.Service.Logins;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.PatientsSheet;
using System;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Consultations
{
    public class ConsultationsFactory : IConsultationsFactory
    {
        #region Fields

        private readonly IConsultationService _consultationServices;
        private readonly IPatientService _patientService;
        private readonly IPatientSheetService _patientSheetService;
        private readonly ILoggerService _logger;
        private readonly IWorkContext _workContext;
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public ConsultationsFactory(
            IConsultationService consultationServices,
            ILoggerService loggerServices, 
            IPatientService patientService, 
            IPatientSheetService patientSheetService, 
            IWorkContext workContext,
            IDateTimeHelper dateTimeHelper)
        {
            _consultationServices = consultationServices;
            _patientService = patientService;
            _patientSheetService = patientSheetService;
            _logger = loggerServices;
            _workContext = workContext;
            _dateTimeHelper = dateTimeHelper;
        }

        #endregion

        /***********************************************************************************/

        #region Methods
        public async Task<ConsultationViewModel> PrepereViewModelAsync(Consultation consultation)
        {
            var patientSheet = await _patientSheetService.GetAsync(consultation.PatientSheetId);
            var patient = await _patientService.GetAsync(patientSheet.PatientId);
            ConsultationViewModel consultationViewModel = new ConsultationViewModel()
            {
                Id = consultation.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PatientSheetId = consultation.PatientSheetId,
                Symptoms = consultation.Symptoms,
                Diagnostic = consultation.Diagnostic,
                Prescriptions = consultation.Prescriptions,
                CreationDate = await _dateTimeHelper.ConvertToUserTimeAsync(consultation.CreationDate),
                Hidden = consultation.Hidden
            };
            return consultationViewModel;
        }

        public async Task<ConsultationDetailsViewModel> PrepereDetailsViewModelAsync(int id)
        {
            try
            {
                var consultation = await _consultationServices.GetAsync(id);
                var patientSheet = await _patientSheetService.GetAsync(consultation.PatientSheetId);
                var patient = await _patientService.GetAsync(patientSheet.PatientId);

                var consultationViewModel = new ConsultationDetailsViewModel()
                {
                    Id = consultation.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    PatientSheetId = consultation.PatientSheetId,
                    Symptoms = consultation.Symptoms,
                    Diagnostic = consultation.Diagnostic,
                    Prescriptions = consultation.Prescriptions,
                    CreationDate = await _dateTimeHelper.ConvertToUserTimeAsync(consultation.CreationDate),
                    Hidden = consultation.Hidden
                };
                return consultationViewModel;
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Consultations} manager, Action: {LogInfo.Read}, Consultation: {id}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new ConsultationDetailsViewModel();
            }
            
        }

        public async Task<ConsultationEditViewModel> PrepereEditViewModelAsync(int id)
        {
            try
            {
                var consultation = await _consultationServices.GetAsync(id);
                var patientSheet = await _patientSheetService.GetAsync(consultation.PatientSheetId);
                var patient = await _patientService.GetAsync(patientSheet.PatientId);

                var consultationViewModel = new ConsultationEditViewModel()
                {
                    Id = consultation.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    PatientSheetId = consultation.PatientSheetId,
                    Symptoms = consultation.Symptoms,
                    Diagnostic = consultation.Diagnostic,
                    Prescriptions = consultation.Prescriptions,
                    CreationDate = await _dateTimeHelper.ConvertToUserTimeAsync(consultation.CreationDate),
                    Hidden = consultation.Hidden
                };
                return consultationViewModel;
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Consultations} manager, Action: {LogInfo.Read}, Consultation: {id}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new ConsultationEditViewModel();
            }
        }

        public async Task<ConsultationCreateViewModel> PrepereCreateViewModelAsync(int id)
        {
            try
            {
                var patientSheet = await _patientSheetService.GetAsync(id);
                var patient = await _patientService.GetAsync(patientSheet.PatientId);

                var consultationViewModel = new ConsultationCreateViewModel()
                {
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    PatientSheetId = id,
                };
                return consultationViewModel;
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Consultations} manager, Action: {LogInfo.Read}, Consultation: {id}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new ConsultationCreateViewModel();
            }
        }

        #endregion
    }
}
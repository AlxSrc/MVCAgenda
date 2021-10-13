using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Logging;
using MVCAgenda.Models.Consultations;
using MVCAgenda.Service.Consultations;
using MVCAgenda.Service.Logins;
using System;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Consultations
{
    public class ConsultationsFactory : IConsultationsFactory
    {
        string user = "admin";

        #region Fields

        private readonly IConsultationService _consultationServices;
        private readonly ILoggerService _logger;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public ConsultationsFactory(IConsultationService consultationServices, ILoggerService loggerServices)
        {
            _consultationServices = consultationServices;
            _logger = loggerServices;
        }

        #endregion

        /***********************************************************************************/

        #region Methods
        public async Task<ConsultationViewModel> PrepereViewModelAsync(Consultation consultation)
        {
            ConsultationViewModel consultationViewModel = new ConsultationViewModel()
            {
                Id = consultation.Id,
                SheetPatientId = consultation.SheetPatientId,
                Symptoms = consultation.Symptoms,
                Diagnostic = consultation.Diagnostic,
                Prescriptions = consultation.Prescriptions,
                CreationDate = consultation.CreationDate,
                Hidden = consultation.Hidden
            };
            return consultationViewModel;
        }

        public async Task<ConsultationDetailsViewModel> PrepereDetailsViewModelAsync(int id)
        {
            try
            {
                var consultation = await _consultationServices.GetAsync(id);
                var consultationViewModel = new ConsultationDetailsViewModel()
                {
                    Id = consultation.Id,
                    SheetPatientId = consultation.SheetPatientId,
                    Symptoms = consultation.Symptoms,
                    Diagnostic = consultation.Diagnostic,
                    Prescriptions = consultation.Prescriptions,
                    CreationDate = consultation.CreationDate,
                    Hidden = consultation.Hidden
                };
                return consultationViewModel;
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Consultations} manager, Action: {LogInfo.Read}, Consultation: {id}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new ConsultationDetailsViewModel();
            }
            
        }

        public async Task<ConsultationEditViewModel> PrepereEditViewModelAsync(int id)
        {
            try
            {
                var consultation = await _consultationServices.GetAsync(id);
                var consultationViewModel = new ConsultationEditViewModel()
                {
                    Id = consultation.Id,
                    SheetPatientId = consultation.SheetPatientId,
                    Symptoms = consultation.Symptoms,
                    Diagnostic = consultation.Diagnostic,
                    Prescriptions = consultation.Prescriptions,
                    CreationDate = consultation.CreationDate,
                    Hidden = consultation.Hidden
                };
                return consultationViewModel;
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Consultations} manager, Action: {LogInfo.Read}, Consultation: {id}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new ConsultationEditViewModel();
            }
        }

        #endregion
    }
}
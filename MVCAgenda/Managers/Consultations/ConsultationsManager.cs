using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Core.Logging;
using MVCAgenda.Factories.Consultations;
using MVCAgenda.Models.Consultations;
using MVCAgenda.Service.Consultations;
using MVCAgenda.Service.Logins;
using System;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Consultations
{
    public class ConsultationsManager : IConsultationsManager
    {
        string user = "admin";

        #region Fields
        private readonly IConsultationService _consultationServices;
        private readonly IConsultationsFactory _consultationFactory;
        private readonly ILoggerService _logger;
        #endregion
        /***********************************************************************************/
        #region Constructor
        public ConsultationsManager(IConsultationService consultationServices, IConsultationsFactory consultationFactory, ILoggerService loggerServices)
        {
            _consultationServices = consultationServices;
            _consultationFactory = consultationFactory;
            _logger = loggerServices;
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
                    SheetPatientId = consultationViewModel.SheetPatientId,
                    Diagnostic = consultationViewModel.Diagnostic,
                    Prescriptions = consultationViewModel.Prescriptions,
                    Symptoms = consultationViewModel.Symptoms,
                    CreationDate = DateTime.Now,
                    Hidden = false
                };

                await _consultationServices.CreateAsync(consultation);

                await _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{user} created consultation: patientSheetId {consultationViewModel.SheetPatientId}",
                    FullMessage = null,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Information,
                    Hidden = false
                });
                return StringHelpers.SuccesMessage;
            }
            catch (Exception exception)
            {
                await _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{user} failed to add consultation: patientSheetId {consultationViewModel.SheetPatientId}",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
                return "Consultatia nu a putut fi adaugata.";
            }
        }

        public async Task<ConsultationDetailsViewModel> GetDetailsAsync(int id)
        {
            try
            {
                return await _consultationFactory.PrepereConsultationDetailsViewModel(await _consultationServices.GetAsync(id));
            }
            catch (Exception exception)
            {
                await _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{user} failed see consultation: id:{id}",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
                return new ConsultationDetailsViewModel();
            }
        }
        public async Task<ConsultationEditViewModel> GetEditDetailsAsync(int id)
        {
            try
            {
                return await _consultationFactory.PrepereConsultationEditDetailsViewModel(await _consultationServices.GetAsync(id));
            }
            catch (Exception exception)
            {
                await _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{user} failed see consultation: id:{id}",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
                return new ConsultationEditViewModel();
            }
        }
        
        public async Task<string> UpdateAsync(ConsultationEditViewModel consultationViewModel)
        {
            try
            {
                if (await CheckExist(consultationViewModel.Id) == false)
                {
                    await _logger.CreateAsync(new Log()
                    {
                        ShortMessage = $"{user} failed to get consultation: id:{consultationViewModel.Id}",
                        FullMessage = null,
                        CreatedOnUtc = DateTime.UtcNow,
                        IpAddress = null,
                        LogLevel = LogLevel.Error,
                        Hidden = false
                    });
                    return "Consultatia nu a putut fi gasita.";
                }
                else
                {
                    var consultation = new Consultation()
                    {
                        Id = consultationViewModel.Id,
                        SheetPatientId = consultationViewModel.SheetPatientId,
                        Diagnostic = consultationViewModel.Diagnostic,
                        Prescriptions = consultationViewModel.Prescriptions,
                        Symptoms = consultationViewModel.Symptoms,
                        CreationDate = consultationViewModel.CreationDate,
                        Hidden = consultationViewModel.Hidden
                    };

                    await _consultationServices.UpdateAsync(consultation);

                    await _logger.CreateAsync(new Log()
                    {
                        ShortMessage = $"{user} edited consultation: id:{consultationViewModel.Id}",
                        FullMessage = null,
                        CreatedOnUtc = DateTime.UtcNow,
                        IpAddress = null,
                        LogLevel = LogLevel.Error,
                        Hidden = false
                    });
                    return StringHelpers.SuccesMessage;
                }

            }
            catch (Exception exception)
            {
                _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{user} failed to edit consultation: id:{consultationViewModel.Id}",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
                return "Consultatia nu a putut fi editata.";
            }

        }
        
        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                if (await CheckExist(id) == false)
                {
                    await _logger.CreateAsync(new Log()
                    {
                        ShortMessage = $"{user} delete consultation: id:{id}",
                        FullMessage = null,
                        CreatedOnUtc = DateTime.UtcNow,
                        IpAddress = null,
                        LogLevel = LogLevel.Error,
                        Hidden = false
                    });
                    return "Consultatia inexistenta.";
                }
                else
                {
                    await _consultationServices.HideAsync(id);
                    await _logger.CreateAsync(new Log()
                    {
                        ShortMessage = $"{user} delete consultation: id:{id}",
                        FullMessage = null,
                        CreatedOnUtc = DateTime.UtcNow,
                        IpAddress = null,
                        LogLevel = LogLevel.Information,
                        Hidden = false
                    });
                    return StringHelpers.SuccesMessage;
                }

            }
            catch (Exception exception)
            {
                await _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{user} failed to delete consultation: id:{id}",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Core.Logging;
using MVCAgenda.Factories.Patients;
using MVCAgenda.Models.Patients;
using MVCAgenda.Service.Logins;
using MVCAgenda.Service.Patients;

namespace MVCAgenda.Managers.Patients
{
    public class PatientsManager : IPatientsManager
    {
        string user = "admin";

        #region Fields
        private readonly IPatientService _patientServices;
        private readonly IPatientsFactory _patientFactory;
        private readonly ILoggerService _logger;
        #endregion
        /***********************************************************************************/
        #region Constructor
        public PatientsManager(IPatientService patientServices, IPatientsFactory patientFactory, ILoggerService loggerServices)
        {
            _patientServices = patientServices;
            _patientFactory = patientFactory;
            _logger = loggerServices;
        }
        #endregion
        /***********************************************************************************/
        #region Methods
        public async Task<string> CreateAsync(PatientViewModel patientViewModel)
        {
            try
            {
                string msg;

                var patients = await _patientServices.GetListAsync(patientViewModel.FirstName, patientViewModel.PhoneNumber, null, false,false);
                    

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
                        Blacklist = patientViewModel.Blacklist,
                        Hidden = false
                    };

                    await _patientServices.CreateAsync(patient);
                    await _logger.CreateAsync(new Log()
                    {
                        ShortMessage = $"{user} created patient: {patientViewModel.FirstName}, {patientViewModel.PhoneNumber}",
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
                    ShortMessage = $"{user} failed to add patient: {patientViewModel.FirstName}, {patientViewModel.PhoneNumber}",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
                return "Pacientul nu a putut fi adaugat.";
            }
        }
        
        public async Task<PatientsViewModel> GetListAsync(string searchByName, string searchByPhoneNumber, string searchByEmail, bool includeBlackList, bool isDeleted)
        {
            try
            {
                var patientsViewModelList = new List<PatientViewModel>();
                var patientsList = await _patientServices.GetListAsync(searchByName, searchByPhoneNumber, searchByEmail, includeBlackList, isDeleted);

                foreach (var patient in patientsList)
                    patientsViewModelList.Add(_patientFactory.PreperePatientViewModel(patient));

                var patientsViewModel = new PatientsViewModel()
                {
                    PatientsList = patientsViewModelList,
                    Hidden = isDeleted,
                    Blacklist = includeBlackList
                };
                return patientsViewModel;
            }
            catch(Exception exception)
            {
                await _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{user} failed to get patients",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
                return new PatientsViewModel();
            }
        }
        
        public async Task<PatientViewModel> GetDetailsAsync(int id)
        {
            try
            {
                return _patientFactory.PreperePatientViewModel(await _patientServices.GetAsync(id));
            }
            catch (Exception exception)
            {
                await _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{user} failed to see patient: id:{id}",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
                return new PatientViewModel();
            }
        }
        
        public async Task<string> UpdateAsync(PatientViewModel patientViewModel)
        {
            try
            {
                if(await CheckExist(patientViewModel.Id) != true)
                {
                    await _logger.CreateAsync(new Log()
                    {
                        ShortMessage = $"{user} failed to see patient: id:{patientViewModel.Id}",
                        FullMessage = null,
                        CreatedOnUtc = DateTime.UtcNow,
                        IpAddress = null,
                        LogLevel = LogLevel.Error,
                        Hidden = false
                    });
                    return "Pacientul nu a putut fi gasit.";
                }
                else
                {
                    Patient patient = new Patient()
                    {
                        Id = patientViewModel.Id,
                        PatientSheetId = patientViewModel.SheetPatientId,
                        FirstName = $"{patientViewModel.FirstName.Substring(0, 1).ToUpper()}{patientViewModel.FirstName.Substring(1, patientViewModel.FirstName.Length - 1).ToLower()}",
                        LastName = patientViewModel.LastName != null ? $"{patientViewModel.LastName.Substring(0, 1).ToUpper()}{patientViewModel.LastName.Substring(1, patientViewModel.LastName.Length - 1).ToLower()}" : null,
                        PhoneNumber = patientViewModel.PhoneNumber,
                        Mail = patientViewModel.Mail,
                        Blacklist = patientViewModel.Blacklist,
                        Hidden = patientViewModel.Hidden
                    };

                    await _patientServices.UpdateAsync(patient);

                    await _logger.CreateAsync(new Log()
                    {
                        ShortMessage = $"{user} edited patient: {patientViewModel.FirstName}, {patientViewModel.PhoneNumber}",
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
                await _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{user} failed to edit patient: {patientViewModel.FirstName}, {patientViewModel.PhoneNumber}",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
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
                    await _logger.CreateAsync(new Log()
                    {
                        ShortMessage = $"{user} failed to see patient: id:{id}",
                        FullMessage = null,
                        CreatedOnUtc = DateTime.UtcNow,
                        IpAddress = null,
                        LogLevel = LogLevel.Error,
                        Hidden = false
                    });
                }
                else
                {
                    await _patientServices.HideAsync(id);
                    await _logger.CreateAsync(new Log()
                    {
                        ShortMessage = $"{user} deleted patient: id:{id}",
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
                await _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{user} failed to delete patient: id:{id}",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
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

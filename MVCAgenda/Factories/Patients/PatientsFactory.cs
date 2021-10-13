using System.Threading.Tasks;
using MVCAgenda.Service.Patients;
using System.Collections.Generic;
using MVCAgenda.Models.Patients;
using MVCAgenda.Core.Domain;
using System;
using MVCAgenda.Service.Logins;
using MVCAgenda.Core.Logging;

namespace MVCAgenda.Factories.Patients
{
    public class PatientsFactory : IPatientsFactory
    {

        string user = "TestLogging";

        #region Fields

        private readonly IPatientService _patientServices;
        private readonly ILoggerService _logger;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public PatientsFactory(IPatientService patientServices, ILoggerService loggerServices)
        {
            _patientServices = patientServices;
            _logger = loggerServices;
        }

        #endregion

        /***********************************************************************************/

        #region Methods

        public async Task<PatientsViewModel> GetListViewModelAsync(string searchByName = null, string searchByPhoneNumber = null, string searchByEmail = null, bool? includeBlackList = null, bool? isDeleted = null)
        {
            try
            {
                var patientsListViewModel = new List<PatientListItemViewModel>();
                var patientsList = await _patientServices.GetListAsync(searchByName, searchByPhoneNumber, searchByEmail, includeBlackList, isDeleted);

                foreach (var patient in patientsList)
                    patientsListViewModel.Add(PrepereListItem(patient));

                var patientsViewModel = new PatientsViewModel()
                {
                    PatientsList = patientsListViewModel,
                    Hidden = isDeleted,
                    Blacklist = includeBlackList
                };
                return patientsViewModel;
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Patients} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new PatientsViewModel();
            }
        }

        public async Task<PatientViewModel> PrepereDetailsViewModelAsync(int id)
        {
            try
            {
                return PrepereViewModel(await _patientServices.GetAsync(id));
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Patients} manager, Action: {LogInfo.Edit}: {id}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new PatientViewModel();
            }
        }

        
        #endregion

        /***********************************************************************************/

        #region Utils

        PatientViewModel PrepereViewModel(Patient patient)
        {
            PatientViewModel viewModel = new PatientViewModel()
            {
                Id = patient.Id,
                SheetPatientId = patient.PatientSheetId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                Mail = patient.Mail,
                Hidden = patient.Hidden
            };
            if (patient.Blacklist == true)
            {
                viewModel.Blacklist = true;
                viewModel.BlacklistText = "<span class=\"badge bg-danger\">Da</span>";
            }
            else if (patient.Blacklist == false)
            {
                viewModel.Blacklist = false;
                viewModel.BlacklistText = "<span class=\"badge bg-success\">Nu</span>";
            }
            else
            {
                viewModel.BlacklistText = "-";
            }

            return viewModel;
        }

        PatientListItemViewModel PrepereListItem(Patient patient)
        {
            PatientListItemViewModel viewModel = new PatientListItemViewModel()
            {
                Id = patient.Id,
                SheetPatientId = patient.PatientSheetId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Mail = patient.Mail,
                PhoneNumber = patient.PhoneNumber,
                Blacklist = patient.Blacklist
            };

            return viewModel;
        }

        #endregion
    }
}
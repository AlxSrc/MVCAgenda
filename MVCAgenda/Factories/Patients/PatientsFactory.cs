using System.Threading.Tasks;
using MVCAgenda.Service.Patients;
using System.Collections.Generic;
using MVCAgenda.Models.Patients;
using MVCAgenda.Core.Domain;
using System;
using MVCAgenda.Service.Logins;
using MVCAgenda.Core.Logging;
using MVCAgenda.Core.Status;
using MVCAgenda.Core.Helpers;

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

        public async Task<PatientsViewModel> GetListViewModelAsync(int pageIndex,
            string searchByName = null, 
            string searchByPhoneNumber = null, 
            string searchByEmail = null, 
            bool? includeBlackList = null, 
            bool? isDeleted = null)
        {
            try
            {
                var patientsList = await _patientServices.GetListAsync(pageIndex, searchByName, searchByPhoneNumber, searchByEmail, includeBlackList, isDeleted);

                var totalPatients = await _patientServices.GetPatientsNumberAsync(searchByName, searchByPhoneNumber, searchByEmail, includeBlackList, isDeleted);
                var totalPages = (int)Math.Ceiling(totalPatients / (double)Constants.TotalItemsOnAPage);

                var patientsListViewModel = new List<PatientListItemViewModel>();
                foreach (var patient in patientsList)
                    patientsListViewModel.Add(PrepereListItem(patient));

                var patientsViewModel = new PatientsViewModel()
                {
                    PageIndex = pageIndex,
                    TotalPages = totalPages,
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
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                Mail = patient.Mail,
                Hidden = patient.Hidden
            };
            if (patient.StatusCode == (int)PatientStatus.Blacklist)
            {
                viewModel.StatusCode = (int)PatientStatus.Blacklist;
                viewModel.BlacklistText = "<span class=\"badge bg-danger\">Blacklist</span>";
            }
            else if (patient.StatusCode == (int)PatientStatus.LoyalPatient)
            {
                viewModel.StatusCode = (int)PatientStatus.LoyalPatient;
                viewModel.BlacklistText = "<span class=\"badge bg-success\">Pacient fidel</span>";
            }
            else
            {
                viewModel.StatusCode = (int)PatientStatus.Patient;
                viewModel.BlacklistText = "Pacient";
            }

            return viewModel;
        }

        PatientListItemViewModel PrepereListItem(Patient patient)
        {
            PatientListItemViewModel viewModel = new PatientListItemViewModel()
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Mail = patient.Mail,
                PhoneNumber = patient.PhoneNumber,
                StatusCode = patient.StatusCode
            };

            return viewModel;
        }

        #endregion
    }
}
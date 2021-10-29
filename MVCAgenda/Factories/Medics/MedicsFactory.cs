using MVCAgenda.Core;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Logging;
using MVCAgenda.Models.Medics;
using MVCAgenda.Service.Logins;
using MVCAgenda.Service.Medics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Medics
{
    public class MedicsFactory : IMedicsFactory
    {
        #region Fields

        private readonly IMedicService _medicsServices;
        private readonly ILoggerService _logger;
        private readonly IWorkContext _workContext;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public MedicsFactory(IMedicService medicsServices, ILoggerService loggerServices, IWorkContext workContext)
        {
            _medicsServices = medicsServices;
            _logger = loggerServices;
            _workContext = workContext;
        }

        #endregion

        /***********************************************************************************/

        #region Methods

        public async Task<List<MedicViewModel>> PrepereListModel(string mail = null, bool? hidden = null)
        {
            try
            {
                var medics = await _medicsServices.GetListAsync(mail, hidden);
                var MedicsViewModel = new List<MedicViewModel>();
                foreach (var medic in medics)
                    MedicsViewModel.Add(PrepereMedicViewModel(medic));

                return MedicsViewModel;
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Medics} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new List<MedicViewModel>();
            }
        }

        public async Task<MedicsViewModel> PrepereMedicsListViewModelAsync()
        {
            try
            {
                var medicsListViewModel = new List<MedicViewModel>();
                var medics = await _medicsServices.GetListAsync();

                foreach (var medic in medics)
                    medicsListViewModel.Add(PrepereMedicViewModel(medic));

                var medicsViewModel = new MedicsViewModel()
                {
                    MedicsList = medicsListViewModel
                };
                return medicsViewModel;
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new MedicsViewModel();
            }
        }

        public async Task<MedicViewModel> PrepereDetailsViewModel(int id)
        {
            try
            {
                return PrepereMedicViewModel(await _medicsServices.GetAsync(id));
            }
            catch (Exception exception)
            {
                var msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Medics} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new MedicViewModel();
            }
        }

        #endregion

        /***********************************************************************************/

        #region Utils

        public static MedicViewModel PrepereMedicViewModel(Medic medic)
        {
            return new MedicViewModel()
            {
                Id = medic.Id,
                Name = medic.Name,
                Mail = medic.Mail,
                ImagePath = medic.ImagePath,
                Designation = medic.Designation,
                Description = medic.Description,
                Hidden = medic.Hidden
            };
            ;
        }

        #endregion
    }
}
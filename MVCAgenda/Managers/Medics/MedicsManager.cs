using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Core.Logging;
using MVCAgenda.Factories.Medics;
using MVCAgenda.Models.Medics;
using MVCAgenda.Service.Logins;
using MVCAgenda.Service.Medics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Medics
{
    public class MedicsManager : IMedicsManager
    {
        string user = "admin";

        #region Fields
        private readonly IMedicService _medicsServices;
        private readonly IMedicsFactory _medicsFactory;
        private readonly ILoggerService _logger;
        #endregion
        /***********************************************************************************/
        #region Constructor
        public MedicsManager(IMedicService medicsServices, IMedicsFactory medicsFactory, ILoggerService loggerServices)
        {
            _medicsServices = medicsServices;
            _medicsFactory = medicsFactory;
            _logger = loggerServices;
        }
        #endregion
        /***********************************************************************************/
        #region Methods
        public async Task<string> CreateAsync(MedicViewModel model)
        {
            try
            {
                var medic = new Medic()
                {
                    Name = model.Name,
                    Mail = model.Mail,
                    ImagePath = model.ImagePath,
                    Designation = model.Designation,
                    Description = model.Description,
                    Hidden = false
                };

                var result = await _medicsServices.CreateAsync(medic);
                if (result == false)
                    return "Medicul nu s-a putut creea";
                else
                {
                    var msg = $"User: {user}, Table:{LogTable.Medics} manager, Action: {LogInfo.Create}, Medic: {medic.Id}";
                    await _logger.CreateAsync(msg, null, null, LogLevel.Information);
                    return StringHelpers.SuccesMessage;
                }

            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Medics} manager, Action: {LogInfo.Create}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return "Medicul nu a putut fi adaugat.";
            }
        }

        public async Task<List<MedicViewModel>> GetListAsync()
        {
            try
            {
                var medics = await _medicsServices.GetListAsync();
                var MedicsViewModel = new List<MedicViewModel>();
                foreach (var medic in medics)
                    if (medic.Hidden == false)
                        MedicsViewModel.Add(await _medicsFactory.PrepereMedicViewModel(medic));

                return MedicsViewModel;
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Medics} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new List<MedicViewModel>();
            }
        }
        public async Task<MedicViewModel> GetDetailsAsync(int id)
        {
            try
            {
                return await _medicsFactory.PrepereMedicViewModel(await _medicsServices.GetAsync(id));
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Medics} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new MedicViewModel();
            }
        }
        
        public async Task<string> UpdateAsync(MedicViewModel model)
        {
            try
            {
                if (await CheckExist(model.Id) == false )
                {
                    return "Medicul nu a putut fi gasit.";
                }
                else
                {
                    var medic = new Medic()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Mail = model.Mail,
                        ImagePath = model.ImagePath,
                        Designation = model.Designation,
                        Description = model.Description,
                        Hidden = false
                    };

                    var result = await _medicsServices.UpdateAsync(medic);
                    if (result == false)
                        return "Medicul nu a putut di editat";
                    else
                    {
                        var msg = $"User: {user}, Table:{LogTable.Medics} manager, Action: {LogInfo.Edit}";
                        await _logger.CreateAsync(msg, null, null, LogLevel.Information);
                        return StringHelpers.SuccesMessage;
                    }
                }

            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Medics} manager, Action: {LogInfo.Edit}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return "Medicul nu a putut fi editat.";
            }

        }

        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                if (await CheckExist(id) == false)
                {
                    return "Medic inexistent.";
                }
                else
                {
                    var result = await _medicsServices.HideAsync(id);
                    if (result == false)
                        return "Medicul nu a putut fi sters.";
                    else
                    {
                        var msg = $"User: {user}, Table:{LogTable.Medics} manager, Action: {LogInfo.Delete}";
                        await _logger.CreateAsync(msg, null, null, LogLevel.Information);
                        return StringHelpers.SuccesMessage;
                    }

                }
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Medics} manager, Action: {LogInfo.Delete}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return "Medicul nu a putut fi stears.";
            }
        }
        #endregion
        /***********************************************************************************/
        #region Utils
        private async Task<bool> CheckExist(int id)
        {
            var model = await _medicsServices.GetAsync(id);

            if (model == null)
                return false;

            return true;
        }
        #endregion
    }
}

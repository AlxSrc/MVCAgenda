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

                await _medicsServices.CreateAsync(medic);

                await _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{user} created medic: name {model.Name}",
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
                    ShortMessage = $"{user} failed to add medic: name {model.Name}",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
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
                await _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{user} failed see medics",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
                return null;
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
                await _logger.CreateAsync(new Log()
                {
                    ShortMessage = $"{user} failed see medic: id:{id}",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
                return null;
            }
        }
        
        public async Task<string> UpdateAsync(MedicViewModel model)
        {
            try
            {
                if (await CheckExist(model.Id) == false )
                {
                    await _logger.CreateAsync(new Log()
                    {
                        ShortMessage = $"{user} failed to get medic: name:{model.Name}",
                        FullMessage = null,
                        CreatedOnUtc = DateTime.UtcNow,
                        IpAddress = null,
                        LogLevel = LogLevel.Error,
                        Hidden = false
                    });
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

                    await _medicsServices.UpdateAsync(medic);

                    await _logger.CreateAsync(new Log()
                    {
                        ShortMessage = $"{user} edited medic: name:{model.Name}",
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
                    ShortMessage = $"{user} failed to edit medic: name:{model.Name}",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
                return "Medicul nu a putut fi editat.";
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
                        ShortMessage = $"{user} failed to get medic: id:{id}",
                        FullMessage = null,
                        CreatedOnUtc = DateTime.UtcNow,
                        IpAddress = null,
                        LogLevel = LogLevel.Error,
                        Hidden = false
                    });
                    return "Medic inexistent.";
                }
                else
                {
                    await _medicsServices.HideAsync(id);

                    await _logger.CreateAsync(new Log()
                    {
                        ShortMessage = $"{user} delete medic: id:{id}",
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
                    ShortMessage = $"{user} failed to delete medic: id:{id}",
                    FullMessage = exception.Message,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = null,
                    LogLevel = LogLevel.Error,
                    Hidden = false
                });
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

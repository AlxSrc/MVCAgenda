using MVCAgenda.Core.Helpers;
using MVCAgenda.Core.Logging;
using MVCAgenda.Factories.Appointments;
using MVCAgenda.Models.Home;
using MVCAgenda.Service.Appointments;
using MVCAgenda.Service.Logins;
using MVCAgenda.Service.Medics;
using System;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Home
{
    public class HomeFactory : IHomeFactory
    {
        string user = "admin";

        #region Fields

        private readonly ILoggerService _logger;
        private readonly IAppointmentService _appointmentServices;
        private readonly IMedicService _medicServices;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public HomeFactory(ILoggerService loggerServices, IAppointmentService appointmentServices, IMedicService medicServices)
        {
            _logger = loggerServices;
            _appointmentServices = appointmentServices;
            _medicServices = medicServices;
        }

        #endregion

        /***********************************************************************************/

        #region Methods

        public async Task<HomeViewModel> PrepereHomeViewModel(string mail = null)
        {
            try
            {
                var dailyAppointmentsCount = await _appointmentServices.GetNumberOfFiltredAppointmentsAsync(Daily:true);
                var dailyPrivateAppointmentsCount = await _appointmentServices.GetNumberOfFiltredAppointmentsAsync(Daily:true, privateAppointment:true);
                var dailyInsuranceAppointmentsCount = await _appointmentServices.GetNumberOfFiltredAppointmentsAsync(Daily:true, privateAppointment:false);

                var dailyPersonalAppointmentsCount = 0;
                if (mail != null && mail != Constants.UserName)
                {
                    var medic = await _medicServices.GetAsync(mail);
                    if(medic != null)
                    {
                        var medicId = medic.Id;
                        dailyPersonalAppointmentsCount = await _appointmentServices.GetNumberOfFiltredAppointmentsAsync(searchByMedic: medicId, Daily:true);
                    }
                }

                return new HomeViewModel() { DailyAppointments = dailyAppointmentsCount, PersonalDailyAppointments = dailyPersonalAppointmentsCount, DailyPrivateAppointments = dailyPrivateAppointmentsCount, DailyInsuranceAppointments = dailyInsuranceAppointmentsCount };
            }
            catch (Exception exception)
            {
                var msg = $"User: {user}, Table:{LogTable.Medics} manager, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, exception.Message, null, LogLevel.Error);
                return new HomeViewModel();
            }
        }

        #endregion

        /***********************************************************************************/

        #region Utils

        

        #endregion

        
    }
}

using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Appointments;
using System;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Appointments
{
    public interface IAppointmentsFactory
    {
        Task<AppointmentEditViewModel> PrepereEditViewModelAsync(int id);

        Task<AppointmentDetailsViewModel> PrepereDetailsViewModelAsync(int id);

        Task<AppointmentsViewModel> PrepereListViewModelAsync(string SearchByName = null,
            string SearchByPhoneNumber = null,
            string SearchByEmail = null,
            DateTime? SearchByAppointmentStartDate = null,
            DateTime? SearchByAppointmentEndDate = null,
            int? SearchByRoom = null,
            int? SearchByMedic = null,
            string SearchByProcedure = null,
            int? Id = null,
            bool? Daily = null,
            bool? Hidden = null);
    }
}
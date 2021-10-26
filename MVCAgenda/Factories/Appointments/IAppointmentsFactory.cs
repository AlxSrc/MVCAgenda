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

        Task<AppointmentsViewModel> PrepereListViewModelAsync(int pageIndex,
            string searchByName = null,
            string searchByPhoneNumber = null,
            string searchByEmail = null,
            DateTime? searchByAppointmentStartDate = null,
            DateTime? searchByAppointmentEndDate = null,
            int? searchByRoom = null,
            int? searchByMedic = null,
            string searchByProcedure = null,
            int? id = null,
            bool? made = null,
            bool? daily = null,
            bool? hidden = null);
    }
}
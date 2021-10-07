using System;
using System.Threading.Tasks;
using MVCAgenda.Models.Appointments;

namespace MVCAgenda.Managers.Appointments
{
    public interface IAppointmentsManager
    {
        Task<string> CreateAsync(AppointmentCreateViewModel appointmentViewModel);
        
        Task<AppointmentsViewModel> GetListAsync(string SearchByName = null,
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

        Task<AppointmentDetailsViewModel> GetDetailsAsync(int id);
        Task<AppointmentEditViewModel> GetEditDetailsAsync(int id);

        Task<string> UpdateAsync(AppointmentEditViewModel appointmentViewModel);
        
        Task<string> DeleteAsync(int id);
    }
}

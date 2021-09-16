using System;
using System.Threading.Tasks;
using MVCAgenda.Models.Appointments;

namespace MVCAgenda.Managers.Appointments
{
    public interface IAppointmentsManager
    {
        Task<string> CreateAsync(AppointmentCreateViewModel appointmentViewModel);
        
        Task<AppointmentsViewModel> GetListAsync(string SearchByName, string SearchByPhoneNumber, string SearchByEmail, DateTime SearchByAppointmentStartDate, DateTime SearchByAppointmentEndDate, int SearchByRoom, int SearchByMedic, string SearchByProcedure, int Id, bool daily, bool Hidden);

        Task<AppointmentDetailsViewModel> GetDetailsAsync(int id);
        Task<AppointmentEditViewModel> GetEditDetailsAsync(int id);

        Task<string> UpdateAsync(AppointmentEditViewModel appointmentViewModel);
        
        Task<string> DeleteAsync(int id);
    }
}

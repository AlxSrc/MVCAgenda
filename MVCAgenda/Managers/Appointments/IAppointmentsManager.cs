using System;
using System.Threading.Tasks;
using MVCAgenda.Models.Appointments;

namespace MVCAgenda.Managers.Appointments
{
    public interface IAppointmentsManager
    {
        Task<string> CreateAsync(AppointmentCreateViewModel appointmentViewModel);

        Task<string> UpdateAsync(AppointmentEditViewModel appointmentViewModel);

        Task<string> DeleteAsync(int id);
    }
}
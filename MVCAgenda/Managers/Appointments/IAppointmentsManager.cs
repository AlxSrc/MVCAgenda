using MVCAgenda.Models.Appointments;
using System.Threading.Tasks;

namespace MVCAgenda.Managers.Appointments
{
    public interface IAppointmentsManager
    {
        Task<string> CreateAsync(AppointmentCreateViewModel appointmentViewModel);
        
        Task<AppointmentsViewModel> GetListAsync(string SearchByName, string SearchByPhoneNumber, string SearchByEmail, string SearchByAppointmentHour, string SearchByAppointmentDate, int SearchByRoom, int SearchByMedic, string SearchByProcedure, int Id, bool daily, bool Hiden);

        Task<AppointmentDetailsViewModel> GetDetailsAsync(int id);
        Task<AppointmentEditViewModel> GetEditDetailsAsync(int id);

        Task<string> UpdateAsync(AppointmentEditViewModel appointmentViewModel);
        
        Task<string> DeleteAsync(int id);
    }
}

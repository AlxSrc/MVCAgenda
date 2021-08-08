using MVCAgenda.Core.Domain;
using MVCAgenda.Core.MVCAgendaManagement;
using MVCAgenda.Core.ViewModels;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Appointments
{
    public interface IAppointmentServices
    {
        Task<string> CreateAppointmentAsync(AppointmentViewModel appointment);
        Task<string> EditAppointmentAsync(AppointmentViewModel appointment);
        Task<string> HideAppointmentAsync(int id);
        Task<bool> DeleteAppointmentAsync(int id);
        Task<AppointmentViewModel> GetAppointmentByIdAsync(int Id);
        Task<AppointmentViewModel> GetAppointmentViewModelByIdAsync(Appointment Appointment);
        Task<MVCAgendaViewsManager> GetAppointmentsAsync(string SearchByName, string SearchByPhoneNumber, string SearchByEmail, string SearchByAppointmentHour, string SearchByAppointmentDate, int SearchByRoom, int SearchByMedic, int Id, bool daily, bool Hiden);
    }
}

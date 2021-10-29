using MVCAgenda.ApiHost.DTOs.Appointments;
using MVCAgenda.Core.Domain;
using System.Threading.Tasks;

namespace MVCAgenda.ApiHost.Factories.Appointments
{
    public interface IAppointmentsFactory
    {
        Task<AppointmentCompleteDataDto> PrepereAppointmentDTO(Appointment appointment);
    }
}

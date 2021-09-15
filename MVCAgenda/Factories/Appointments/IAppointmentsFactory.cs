using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Appointments;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Appointments
{
    public interface IAppointmentsFactory
    {
        Task<AppointmentViewModel> PrepereAppointmentViewModel(Appointment model, Patient patient, Medic medic, Room room);
        
        Task<AppointmentListItemViewModel> PrepereAppointmentListItemViewModel(Appointment model, Patient patient, Medic medic, Room room);
        
        Task<AppointmentDetailsViewModel> PrepereAppointmentDetailsViewModel(Appointment model, Patient patient, Medic medic, Room room);
        Task<AppointmentEditViewModel> PrepereAppointmentEditDetailsViewModel(Appointment model);
        
    }
}

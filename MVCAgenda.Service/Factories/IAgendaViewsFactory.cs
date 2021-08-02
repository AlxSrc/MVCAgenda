using MVCAgenda.Core.Domain;
using MVCAgenda.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Factories
{
    public interface IAgendaViewsFactory
    {
        //Patient
        PatientViewModel PreperePatientViewModel(Patient model);

        //SheetPatient
        Task<SheetPatientViewModel> PrepereSheetPatientViewModel(SheetPatient model, List
            <Consultation> consultations);

        //Appointment
        AppointmentViewModel PrepereAppointmentViewModel(Appointment model, Patient patient, Medic medic, Room room);
    }
}

using MVCAgenda.ApiHost.DTOs.Appointments;
using MVCAgenda.ApiHost.DTOs.Consultations;
using MVCAgenda.ApiHost.DTOs.Patients;
using MVCAgenda.ApiHost.DTOs.PatientSheets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.ApiHost.Factories.Items
{
    public interface IItemsFactory
    {
        Task<List<PatientDto>> PreperePatientsList();
        Task<List<PatientSheetDto>> PreperePatientSheetsList();
        Task<List<ConsultationDto>> PrepereConsultationsList();
        Task<List<AppointmentDto>> PrepereAppointmentsList();
    }
}

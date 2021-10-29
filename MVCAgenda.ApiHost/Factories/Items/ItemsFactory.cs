using MVCAgenda.ApiHost.DTOs.Appointments;
using MVCAgenda.ApiHost.DTOs.Consultations;
using MVCAgenda.ApiHost.DTOs.Patients;
using MVCAgenda.ApiHost.DTOs.PatientSheets;
using MVCAgenda.Service.Appointments;
using MVCAgenda.Service.Consultations;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.PatientsSheet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.ApiHost.Factories.Items
{
    public class ItemsFactory : IItemsFactory
    {

        private readonly IPatientService _patientService;
        private readonly IPatientSheetService _patientSheetService;
        private readonly IConsultationService _consultationService;
        private readonly IAppointmentService _appointmentService;

        public ItemsFactory(IPatientService patientService,
            IPatientSheetService patientSheetService,
            IConsultationService consultationService,
            IAppointmentService appointmentService)
        {
            _patientService = patientService;
            _patientSheetService = patientSheetService;
            _consultationService = consultationService;
            _appointmentService = appointmentService;
        }

        public async Task<List<PatientDto>> PreperePatientsList()
        {
            var patients = await _patientService.GetListAsync(-1);
            var list = new List<PatientDto>();

            foreach (var patient in patients)
                list.Add(new PatientDto() 
                { 
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    Mail = patient.Mail,
                    Hidden = patient.Hidden,
                    StatusCode = patient.StatusCode
                });

            return list;
        }
        public async Task<List<PatientSheetDto>> PreperePatientSheetsList()
        {
            var patientSheets = await _patientSheetService.GetListAsync();
            var list = new List<PatientSheetDto>();

            foreach (var patientSheet in patientSheets)
                list.Add(new PatientSheetDto()
                {
                    Id = patientSheet.Id,
                    PatientId = patientSheet.Id,
                    AntecedentsH = patientSheet.AntecedentsH,
                    AntecedentsP = patientSheet.AntecedentsP,
                    DateOfBirth= patientSheet.DateOfBirth,
                    Gender = patientSheet.Gender,
                    NationalIdentificationNumber = patientSheet.NationalIdentificationNumber,
                    PhysicalExamination = patientSheet.PhysicalExamination,
                    Street = patientSheet.Street,
                    Town = patientSheet.Town,
                    Hidden = patientSheet.Hidden
                });

            return list;
        }
        public async Task<List<ConsultationDto>> PrepereConsultationsList()
        {
            var consultations = await _consultationService.GetListAsync(-1);
            var list = new List<ConsultationDto>();

            foreach (var consultation in consultations)
                list.Add(new ConsultationDto()
                {
                    Id = consultation.Id,
                    PatientSheetId = consultation.PatientSheetId,
                    Diagnostic = consultation.Diagnostic,
                    Prescriptions = consultation.Prescriptions,
                    Symptoms = consultation.Symptoms,
                    CreationDate = consultation.CreationDate,
                    Hidden = consultation.Hidden
                });

            return list;
        }
        public async Task<List<AppointmentDto>> PrepereAppointmentsList()
        {
            var appointments = await _appointmentService.GetFiltredListAsync(-1);
            var list = new List<AppointmentDto>();

            foreach (var appointment in appointments)
                list.Add(new AppointmentDto()
                {
                    Id = appointment.Id,
                    PatientId = appointment.PatientId,
                    RoomId = appointment.RoomId,
                    MedicId = appointment.MedicId,

                    Procedure = appointment.Procedure,
                    StartDate = appointment.StartDate,
                    EndDate = appointment.EndDate,
                    Made = appointment.Made,
                    Comments = appointment.Comments,

                    AppointmentCreationDate = appointment.AppointmentCreationDate,
                    ResponsibleForAppointment = appointment.ResponsibleForAppointment,
                    Hidden = appointment.Hidden
                });

            return list;
        }
    }
}

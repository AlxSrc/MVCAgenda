using MVCAgenda.ApiHost.DTOs.Items;
using MVCAgenda.Core.Domain;
using MVCAgenda.Service.Appointments;
using MVCAgenda.Service.Consultations;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.PatientsSheet;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.ApiHost.Managers
{
    public class ItemsManager : IItemsManager
    {
        private readonly IPatientService _patientService;
        private readonly IPatientSheetService _patientSheetService;
        private readonly IConsultationService _consultationService;
        private readonly IAppointmentService _appointmentService;

        public ItemsManager(IPatientService patientService,
            IPatientSheetService patientSheetService,
            IConsultationService consultationService,
            IAppointmentService appointmentService)
        {
            _patientService = patientService;
            _patientSheetService = patientSheetService;
            _consultationService = consultationService;
            _appointmentService = appointmentService;
        }

        public async Task<bool> PostData(ItemsRootObject root)
        {
            try
            {
                var patients = root.Patients;
                foreach (var patient in patients)
				{
                    await _patientService.CreateAsync(new Patient()
                    {
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        PhoneNumber = patient.PhoneNumber,
                        Mail = patient.Mail,
                        StatusCode = patient.StatusCode,
                        Hidden = patient.Hidden
                    });
                }

                var patientSheets = root.PatientSheets;
                foreach (var patientSheet in patientSheets)
                {
                    await _patientSheetService.CreateAsync(new PatientSheet()
                    {
                        Id = patientSheet.Id,
                        PatientId = patientSheet.Id,
                        AntecedentsH = patientSheet.AntecedentsH,
                        AntecedentsP = patientSheet.AntecedentsP,
                        DateOfBirth = patientSheet.DateOfBirth,
                        Gender = patientSheet.Gender,
                        NationalIdentificationNumber = patientSheet.NationalIdentificationNumber,
                        PhysicalExamination = patientSheet.PhysicalExamination,
                        Street = patientSheet.Street,
                        Town = patientSheet.Town,
                        Hidden = patientSheet.Hidden
                    });
                }

                var consultations = root.Consultations;
                foreach (var consultation in consultations)
                {
                    await _consultationService.CreateAsync(new Consultation()
                    {
                        Id = consultation.Id,
                        PatientSheetId = consultation.PatientSheetId,
                        Diagnostic = consultation.Diagnostic,
                        Prescriptions = consultation.Prescriptions,
                        Symptoms = consultation.Symptoms,
                        CreationDate = consultation.CreationDate,
                        Hidden = consultation.Hidden
                    });
                }

                var appointments = root.Appointments;
                foreach (var appointment in appointments)
                {
                    await _appointmentService.CreateAsync(new Appointment()
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
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

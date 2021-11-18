using MVCAgenda.ApiHost.DTOs.Consultations;
using MVCAgenda.ApiHost.DTOs.PatientSheets;
using MVCAgenda.Core.Domain;
using MVCAgenda.Service.Consultations;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.PatientsSheet;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.ApiHost.Factories.PatientSheets
{
    public class PatientSheetFactory : IPatientSheetFactory
    {
        #region Fields

        private readonly IPatientSheetService _patientSheetService;
        private readonly IPatientService _patientService;
        private readonly IConsultationService _consultationService;

        #endregion

        #region Constructor

        public PatientSheetFactory(IPatientSheetService patientSheetService,
            IPatientService patientService,
            IConsultationService consultationService)
        {
            _patientSheetService = patientSheetService;
            _patientService = patientService;
            _consultationService = consultationService;
        }

        #endregion

        public async Task<PatientSheetDto> PrepereDtoAsync(int id)
        {
            try
            {
                var patientDomain = await _patientService.GetAsync(id);
                var patientSheetDomain = await _patientSheetService.GetAsync(id);
                var consultations = await _consultationService.GetListAsync(id);
                
                var consultationsAsDto = new List<ConsultationDto>();
                foreach(var consultation in consultations)
                    consultationsAsDto.Add(PrepereConsultation(consultation));

                var patientSheet = new PatientSheetDto() { 
                    Id = patientSheetDomain.Id,
                    PatientId = patientSheetDomain.PatientId,
                    
                    FirstName = patientDomain.FirstName,
                    LastName = patientDomain.LastName,

                    AntecedentsH = patientSheetDomain.AntecedentsH,
                    AntecedentsP = patientSheetDomain.AntecedentsP,
                    PhysicalExamination = patientSheetDomain.PhysicalExamination,
                    NationalIdentificationNumber = patientSheetDomain.NationalIdentificationNumber,
                    Gender = patientSheetDomain.Gender,
                    DateOfBirth = patientSheetDomain.DateOfBirth,
                    Street = patientSheetDomain.Street,
                    Town = patientSheetDomain.Town,

                    Consultations = consultationsAsDto,

                    Hidden = patientSheetDomain.Hidden
                };


                return patientSheet;
            }
            catch (Exception ex)
            {
                return new PatientSheetDto();
            }
        }


        #region Utils

        ConsultationDto PrepereConsultation(Consultation  consultation)
        {
            return new ConsultationDto()
            {
                Id = consultation.Id,
                PatientSheetId = consultation.PatientSheetId,
                Diagnostic = consultation.Diagnostic,
                Prescriptions = consultation.Prescriptions,
                Symptoms = consultation.Symptoms,
                CreationDate = consultation.CreationDate,
                Hidden = consultation.Hidden
            };
        }

        #endregion

    }
}

using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Consultations;
using MVCAgenda.Models.PatientSheets;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCAgenda.Service.Patients;

namespace MVCAgenda.Factories.PatientsSheet
{
    public class PatientsSheetsFactory : IPatientsSheetsFactory
    {
        private readonly IPatientService _patientService;

        public PatientsSheetsFactory(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public PatientSheetDetailsViewModel PreperePatientSheetDetailsViewModel(PatientSheet model, Patient patient, List
            <ConsultationViewModel> consultations)
        {
            PatientSheetDetailsViewModel preparedView = new PatientSheetDetailsViewModel()
            {
                Id = model.Id,
                PatientId = patient.Id,
                FirstName = patient.FirstName,
                SecondName = patient.LastName,
                AntecedentsH = model.AntecedentsH,
                AntecedentsP = model.AntecedentsP,
                PhysicalExamination = model.PhysicalExamination,
                NationalIdentificationNumber = model.NationalIdentificationNumber,
                DateOfBirth = model.DateOfBirth,
                Town = model.Town,
                Street = model.Street,
                Consultations = consultations,
                Hidden = model.Hidden
            };

            if (model.Gender == 1)
            {
                preparedView.Gender = "Masculin";
            }
            else if (model.Gender == 0)
            {
                preparedView.Gender = "Feminin";
            }
            else
            {
                preparedView.Gender = "-";
            }

            return preparedView;
        }

        public PatientSheetEditViewModel PreperePatientSheetEditViewModel(PatientSheet model)
        {
            return new PatientSheetEditViewModel()
            {
                Id = model.Id,
                AntecedentsH = model.AntecedentsH,
                AntecedentsP = model.AntecedentsP,
                PhysicalExamination = model.PhysicalExamination,
                NationalIdentificationNumber = model.NationalIdentificationNumber,
                DateOfBirth = model.DateOfBirth,
                Town = model.Town,
                Street = model.Street,
                Hidden = model.Hidden
            };
        }
    }
}
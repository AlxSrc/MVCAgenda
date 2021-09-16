using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Consultations;
using MVCAgenda.Models.PatientSheets;
using System.Collections.Generic;

namespace MVCAgenda.Factories.PatientsSheet
{
    public interface IPatientsSheetsFactory
    {
        PatientSheetDetailsViewModel PreperePatientSheetDetailsViewModel(PatientSheet model, Patient patient, List
            <ConsultationViewModel> consultations);
        PatientSheetEditViewModel PreperePatientSheetEditViewModel(PatientSheet model);
    }
}

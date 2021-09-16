using System.Threading.Tasks;
using MVCAgenda.Service.Patients;
using System.Collections.Generic;
using MVCAgenda.Models.Patients;
using MVCAgenda.Core.Domain;

namespace MVCAgenda.Factories.Patients
{
    public class PatientsFactory : IPatientsFactory
    {
        public PatientViewModel PreperePatientViewModel(Patient patient)
        {
            PatientViewModel viewModel = new PatientViewModel()
            {
                Id = patient.Id,
                SheetPatientId = patient.PatientSheetId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                Mail = patient.Mail,
                Hidden = patient.Hidden
            };
            if (patient.Blacklist == true)
            {
                viewModel.Blacklist = true;
                viewModel.BlacklistText = "<span class=\"badge bg-danger\">Da</span>";
            }
            else if (patient.Blacklist == false)
            {
                viewModel.Blacklist = false;
                viewModel.BlacklistText = "<span class=\"badge bg-success\">Nu</span>";
            }
            else
            {
                viewModel.BlacklistText = "-";
            }

            return viewModel;
        }
    }
}

using MVCAgenda.Core.Domain;
using MVCAgenda.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Factories
{
    public class AgendaViewsFactory : IAgendaViewsFactory
    {
        public PatientViewModel PreperePatientViewModel(Patient model)
        {
            PatientViewModel viewModel = new PatientViewModel()
            {
                Id = model.Id,
                SheetPatientId = model.SheetPatientId,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                PhonNumber = model.PhonNumber,
                Mail = model.Mail,
                Hidden = model.Hidden
            };
            if (model.Blacklist == true)
            {
                viewModel.Blacklist = true;
                viewModel.BlacklistText = "<span class=\"badge bg-danger\">Da</span>";
            }
            else if (model.Blacklist == false)
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

        public async Task<SheetPatientViewModel> PrepereSheetPatientViewModel(SheetPatient model, List
            <ConsultationViewModel> consultations)
        {
            SheetPatientViewModel preparedView = new SheetPatientViewModel()
            {
                Id = model.Id,
                AntecedentsH = model.AntecedentsH,
                AntecedentsP = model.AntecedentsP,
                PhysicalExamination = model.PhysicalExamination,
                CNP = model.CNP,
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

        public async Task<ConsultationViewModel> PrepereConsultationViewModel(Consultation consultation)
        {
            ConsultationViewModel consultationViewModel = new ConsultationViewModel() {
                Id = consultation.Id,
                SheetPatientId = consultation.SheetPatientId,
                Symptoms = consultation.Symptoms,
                Diagnostic = consultation.Diagnostic,
                Prescriptions = consultation.Prescriptions,
                CreationDate = consultation.CreationDate,
                Hidden = consultation.Hidden
            };
            return consultationViewModel;
        }

        public AppointmentViewModel PrepereAppointmentViewModel(Appointment model, Patient patient, Medic medic, Room room)
        {
            string WantedDate = $"{model.AppointmentDate.Substring(5, 2)}-{model.AppointmentDate.Substring(8, 2)}-{model.AppointmentDate.Substring(0, 4)}";

            AppointmentViewModel viewModel = new AppointmentViewModel()
            {
                Id = model.Id,
                PatientId = model.PatientId,
                FirstName = patient.FirstName,
                SecondName = patient.SecondName,
                PhonNumber = patient.PhonNumber,
                Mail = patient.Mail,
                Medic = medic.MedicName,
                Room = room.RoomName,
                AppointmentDate = WantedDate,
                AppointmentHour = model.AppointmentHour,
                Procedure = model.Procedure,
                ResponsibleForAppointment = model.ResponsibleForAppointment,
                AppointmentCreationDate = model.AppointmentCreationDate,
                Comments = model.Comments,
                Hidden = model.Hidden
            };

            if (model.Made == true)
            {
                viewModel.Made = true;
                viewModel.MadeText = "<span class=\"badge bg-success\">Da</span>";
            }
            else
            {
                viewModel.Made = false;
                viewModel.MadeText = "<span class=\"badge bg-danger\">Nu</span>";
            }

            if (patient.Blacklist == true)
            {
                viewModel.Blacklist = "<span class=\"badge bg-Danger\">Da</span>";
            }
            else if (patient.Blacklist == false)
            {
                viewModel.Blacklist = "<span class=\"badge bg-success\">Nu</span>";
            }

            return viewModel;
        }

        
    }
}

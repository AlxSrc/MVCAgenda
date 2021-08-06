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
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                PhonNumber = model.PhonNumber,
                Mail = model.Mail,
                SheetPatientId = model.SheetPatientId
            };
            if (model.Blacklist == 1)
            {
                viewModel.Blacklist = "<span class=\"badge bg-danger\">Da</span>";
            }
            else if (model.Blacklist == 0)
            {
                viewModel.Blacklist = "<span class=\"badge bg-success\">Nu</span>";
            }
            else
            {
                viewModel.Blacklist = "-";
            }

            if (model.Visible >= 1)
            {
                viewModel.Visible = true;
            }
            else if (model.Visible == 0)
            {
                viewModel.Visible = false;
            }
            return viewModel;
        }

        public async Task<SheetPatientViewModel> PrepereSheetPatientViewModel(SheetPatient model, List
            <Consultation> consultations)
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
                Consultations = consultations
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
                CreationDate = consultation.CreationDate
            };
            return consultationViewModel;
        }

        public AppointmentViewModel PrepereAppointmentViewModel(Appointment model, Core.Domain.Patient patient, Medic medic, Room room)
        {
            AppointmentViewModel viewModel = new AppointmentViewModel()
            {
                Id = model.Id,
                PatientId = model.PatientId,
                FirstName = patient.FirstName,
                SecondName = patient.SecondName,
                PhonNumber = patient.PhonNumber,
                Mail = patient.Mail,
                Medic = medic.MedicName,
                Camera = room.RoomName,
                AppointmentDate = model.AppointmentDate,
                AppointmentHour = model.AppointmentHour,
                Procedure = model.Procedure,
                ResponsibleForAppointment = model.ResponsibleForAppointment,
                AppointmentCreationDate = model.AppointmentCreationDate,
                Comments = model.Comments
            };

            if (model.Made == 1)
            {
                viewModel.Made = true;
                viewModel.MadeText = "<span class=\"badge bg-success\">Da</span>";
            }
            else if (model.Made == 0)
            {
                viewModel.Made = false;
                viewModel.MadeText = "<span class=\"badge bg-danger\">Nu</span>";
            }
            else
            {
                viewModel.MadeText = "-";
            }

            if (patient.Blacklist == 1)
            {
                viewModel.Blacklist = "<span class=\"badge bg-Danger\">Da</span>";
            }
            else if (patient.Blacklist == 0)
            {
                viewModel.Blacklist = "<span class=\"badge bg-success\">Nu</span>";
            }
            else
            {
                viewModel.Blacklist = "-";
            }

            if (model.Visible >= 1)
            {
                viewModel.Visible = true;
            }
            else if (model.Visible == 0)
            {
                viewModel.Visible = false;
            }
            return viewModel;
        }

        
    }
}

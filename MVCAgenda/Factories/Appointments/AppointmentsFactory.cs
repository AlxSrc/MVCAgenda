using MVCAgenda.Core.Domain;
using MVCAgenda.Models.Appointments;
using System.Threading.Tasks;
using MVCAgenda.Service.Patients;

namespace MVCAgenda.Factories.Appointments
{
    public class AppointmentsFactory : IAppointmentsFactory
    {
        private readonly IPatientService _patientService;

        public AppointmentsFactory(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<AppointmentViewModel> PrepereAppointmentViewModel(Appointment model, Patient patient, Medic medic, Room room)
        {
            var viewModel = new AppointmentViewModel()
            {
                Id = model.Id,
                PatientId = model.PatientId,
                FirstName = patient.FirstName,
                SecondName = patient.LastName,
                PhonNumber = patient.PhoneNumber,
                Mail = patient.Mail,
                Medic = medic.Name,
                MedicId = medic.Id,
                Room = room.Name,
                RoomId = room.Id,
                StartDate = model.StartDate,
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

        public async Task<AppointmentListItemViewModel> PrepereAppointmentListItemViewModel(Appointment model, Patient patient, Medic medic, Room room)
        {
            var viewModel = new AppointmentListItemViewModel()
            {
                Id = model.Id,
                PatientId = model.PatientId,
                FirstName = patient.FirstName,
                PhoneNumber = patient.PhoneNumber,
                Medic = medic.Name,
                Room = room.Name,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Procedure = model.Procedure,
                Hidden = model.Hidden
            };

            if (model.Made == true)
                viewModel.Procedure = $"<span class=\"text-success\">{model.Procedure}</span>";
            else
                viewModel.Procedure = $"<span class=\"text-danger\">{model.Procedure}</span>";

            return viewModel;
        }

        public async Task<AppointmentDetailsViewModel> PrepereAppointmentDetailsViewModel(Appointment model, Patient patient, Medic medic, Room room)
        {
            var viewModel = new AppointmentDetailsViewModel()
            {
                Id = model.Id,
                PatientId = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                Mail = patient.Mail,
                Medic = medic.Name,
                Room = room.Name,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                Procedure = model.Procedure,
                ResponsibleForAppointment = model.ResponsibleForAppointment,
                AppointmentCreationDate = model.AppointmentCreationDate,
                Comments = model.Comments,
                Hidden = model.Hidden
            };

            if (model.Made == true)
            {
                viewModel.MadeText = "<span class=\"badge bg-success\">Da</span>";
            }
            else
            {
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

        public async Task<AppointmentEditViewModel> PrepereAppointmentEditDetailsViewModel(Appointment model)
        {
            var patient = await _patientService.GetAsync(model.PatientId);

            var viewModel = new AppointmentEditViewModel()
            {
                Id = model.Id,
                PatientId = model.PatientId,
                PatientName = $"{patient.FirstName.ToUpper()} {patient.LastName}",
                MedicId = model.MedicId,
                RoomId = model.RoomId,
                Made = model.Made,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                Procedure = model.Procedure,
                ResponsibleForAppointment = model.ResponsibleForAppointment,
                AppointmentCreationDate = model.AppointmentCreationDate,
                Comments = model.Comments,
                Hidden = model.Hidden
            };

            return viewModel;
        }
    }
}
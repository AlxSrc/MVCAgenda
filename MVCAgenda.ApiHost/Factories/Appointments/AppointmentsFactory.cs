using MVCAgenda.ApiHost.DTOs.Appointments;
using MVCAgenda.Core.Domain;
using MVCAgenda.Service.Medics;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.Rooms;
using System.Threading.Tasks;

namespace MVCAgenda.ApiHost.Factories.Appointments
{
    public class AppointmentsFactory: IAppointmentsFactory
    {

        #region Fields
        private readonly IPatientService _patientService;
        private readonly IRoomService _roomService;
        private readonly IMedicService _medicService;
        #endregion

        #region Constructor

        public AppointmentsFactory(IPatientService patientService,
            IRoomService roomService, 
            IMedicService medicService)
        {
            _patientService = patientService;
            _roomService = roomService;
            _medicService = medicService;
        }

        #endregion

        public async Task<AppointmentDto> PrepereAppointmentDTO(Appointment appointment)
        {
            var patient = await _patientService.GetAsync(appointment.PatientId);
            var room = await _roomService.GetAsync(appointment.RoomId);
            var medic = await _medicService.GetAsync(appointment.MedicId);

            var appointmentDto = new AppointmentDto()
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                RoomId = appointment.RoomId,
                MedicId = appointment.MedicId,

                FirstName = patient.FirstName, 
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                Mail = patient.Mail,

                MedicName = medic.Name,
                RoomName = room.Name,

                Made = appointment.Made,
                Procedure = appointment.Procedure,
                Comments = appointment.Comments,
                StartDate= appointment.StartDate,
                EndDate= appointment.EndDate,
                
                ResponsibleForAppointment = appointment.ResponsibleForAppointment,
                AppointmentCreationDate = appointment.AppointmentCreationDate
            };
            return appointmentDto;
        }
    }
}

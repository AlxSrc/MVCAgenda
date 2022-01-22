using MVCAgenda.Core.Domain;
using MVCAgenda.Models.SyncfusionScheduler;
using MVCAgenda.Service.Helpers;
using System;
using System.Threading.Tasks;

namespace MVCAgenda.Factories.Scheduler
{
    public class SchedulerFactory : ISchedulerFactory
    {
        #region Fields

        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public SchedulerFactory(
            IDateTimeHelper dateTimeHelper)
        {
            _dateTimeHelper = dateTimeHelper;
        }

        #endregion

        /***********************************************************************************/

        #region Methods
        public async Task<ScheduleEventData> PrepereScheduleItemListViewModel(Appointment appointment, Patient patient, Medic medic, Room room)
        {
            try
            {
                return new ScheduleEventData()
                {
                    Id = appointment.Id,
                    MedicId = appointment.MedicId,
                    RoomId = appointment.RoomId,
                    PatientId = appointment.PatientId,

                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    Mail = patient.Mail,

                    StartTime = await _dateTimeHelper.ConvertToUserTimeAsync(appointment.StartDate),
                    EndTime = await _dateTimeHelper.ConvertToUserTimeAsync(appointment.EndDate),

                    Medic = medic.Name,
                    Room = room.Name,
                    PrimaryColor = room.PrimaryColor,
                    SecondaryColor = room.SecondaryColor,

                    Subject = appointment.Procedure,
                    Description = appointment.Comments,

                    Made = appointment.Made,
                    AppointmentCreationDate = appointment.AppointmentCreationDate,
                    ResponsibleForAppointment = appointment.ResponsibleForAppointment
                };
            }
            catch
            {
                return new ScheduleEventData();
            }
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Patients;

namespace MVCAgenda.Service.Appointments
{
    public class AppointmentServices : IAppointmentServices
    {
        #region Fields
        private readonly AgendaContext _context;
        private readonly IPatientServices _patientServices;
        private string DayTime = DateTime.Now.ToString("yyyy-MM-dd");
        #endregion
        /**************************************************************************************/
        #region Constructor
        public AppointmentServices(AgendaContext context, IPatientServices patientServices)
        {
            _context = context;
            _patientServices = patientServices;
        }
        #endregion
        /**************************************************************************************/
        #region Methods
        public async Task<bool> CreateAsync(Appointment appointment)
        {
            try
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Appointment> GetAsync(int Id)
        {
            return await _context.Appointments.FirstOrDefaultAsync(a => a.Id == Id);
        }

        public async Task<List<Appointment>> GetListAsync(string SearchByAppointmentHour, string SearchByAppointmentDate, int SearchByRoom, int SearchByMedic, string SearchByProcedure, int Id, bool Daily, bool Hidden)
        {
            var appointmentsList = await (
                    _context.Appointments

                        .Where(h => Hidden == true ? h.Hidden == true : h.Hidden == false)
                        .Where(d => Daily == true ? d.AppointmentDate.Contains(DayTime) : true)
                        .Where(p => Id != 0 ? p.PatientId == Id : true)
                        .Where(a => SearchByMedic != 0 ? a.MedicId == SearchByMedic : true)
                        .Where(a => SearchByRoom != 0 ? a.RoomId == SearchByRoom : true)
                        .Where(a => !string.IsNullOrEmpty(SearchByAppointmentHour) ? a.AppointmentHour.Contains(SearchByAppointmentHour) : true)
                        .Where(a => !string.IsNullOrEmpty(SearchByAppointmentDate) ? a.AppointmentDate.Contains(SearchByAppointmentDate) : true)
                        .Where(a => !string.IsNullOrEmpty(SearchByProcedure) ? a.Procedure.ToUpper().Contains(SearchByProcedure.ToUpper()) : true)

                        .OrderBy(h => h.AppointmentHour)

                    ).ToListAsync();

            return appointmentsList;
        }
        
        public async Task<bool> UpdateAsync(Appointment appointment)
        {
            try
            {
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        
        public async Task<bool> HideAsync(int id)
        {
            try
            {
                var appointment = await _context.Appointments.FindAsync(id);
                appointment.Hidden = true;
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _context.Appointments.Remove(await _context.Appointments.FindAsync(id));
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
        /**************************************************************************************/
        #region Utils
        public async Task<string> SearchAppointmentAsync(int MedicId, int RoomId, string AppointmentDate, string AppointmentHour)
        {
            string foundAppointment = StringHelpers.SuccesMessage;

            //Search a medic, date, h
            var appointmentsM = await _context.Appointments
                        .Where(p => p.MedicId == MedicId)
                        .Where(p => p.AppointmentDate == AppointmentDate)
                        .Where(p => p.AppointmentHour == AppointmentHour)
                        .Where(p => p.Hidden == false)
                        .ToListAsync();

            if (appointmentsM.Count >= 1)
            {
                var medic = await _context.Medics.FirstOrDefaultAsync(m => m.Id == MedicId);
                foundAppointment = $"{medic.Name} este ocupat/a la data {AppointmentDate}, ora {AppointmentHour}.";
                return foundAppointment;
            }

            //Search a room, date, h
            var appointmentsR = await _context.Appointments
                        .Where(p => p.RoomId == RoomId)
                        .Where(p => p.AppointmentDate == AppointmentDate)
                        .Where(p => p.AppointmentHour == AppointmentHour)
                        .Where(p => p.Hidden == false)
                        .ToListAsync();

            if (appointmentsR.Count >= 1)
            {
                var room = await _context.Rooms.FirstOrDefaultAsync(m => m.Id == RoomId);
                foundAppointment = $"{room.Name} este ocupata la data {AppointmentDate}, ora {AppointmentHour}.";
                return foundAppointment;
            }

            return foundAppointment;
        }
        #endregion
    }
}

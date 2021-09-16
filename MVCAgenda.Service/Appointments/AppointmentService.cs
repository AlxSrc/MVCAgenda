using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Data.DataBaseManager;

namespace MVCAgenda.Service.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        #region Fields
        private readonly AgendaContext _context;
        #endregion
        /**************************************************************************************/
        #region Constructor
        public AppointmentService(AgendaContext context)
        {
            _context = context;
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

        public async Task<List<Appointment>> GetListAsync(DateTime searchByAppointmentStartDate, DateTime searchByAppointmentEndDate, int SearchByRoom, int SearchByMedic, string SearchByProcedure, int Id, bool Daily, bool Hidden)
        {
            var appointmentsList = await (
                    _context.Appointments

                        .Where(h => Hidden == true ? h.Hidden == true : h.Hidden == false)
                        .Where(d => Daily == true ? d.StartDate.Date == DateTime.Now.Date : true)
                        .Where(p => Id != 0 ? p.PatientId == Id : true)
                        .Where(a => SearchByMedic != 0 ? a.MedicId == SearchByMedic : true)
                        .Where(a => SearchByRoom != 0 ? a.RoomId == SearchByRoom : true)
                        .Where(a => searchByAppointmentStartDate != DateTime.MinValue ? a.StartDate == searchByAppointmentStartDate : true)
                        .Where(a => searchByAppointmentEndDate != DateTime.MinValue ? a.EndDate == searchByAppointmentEndDate : true)
                        .Where(a => !string.IsNullOrEmpty(SearchByProcedure) ? a.Procedure.ToUpper().Contains(SearchByProcedure.ToUpper()) : true)

                        .OrderBy(h => h.StartDate)

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
        public async Task<string> SearchAppointmentAsync(int medicId, int roomId, DateTime startDate, DateTime endDate)
        {
            string foundAppointment = StringHelpers.SuccesMessage;

            //Search a medic, date, h
            var appointmentsM = await _context.Appointments
                        .Where(p => p.MedicId == medicId)
                        .Where(p => p.StartDate == startDate)
                        //.Where(p => p.EndDate == endDate)
                        .Where(p => p.Hidden == false)
                        .ToListAsync();

            if (appointmentsM.Count >= 1)
            {
                var medic = await _context.Medics.FirstOrDefaultAsync(m => m.Id == medicId);
                foundAppointment = $"{medic.Name} este ocupat/a la data {startDate}.";
                return foundAppointment;
            }

            //Search a room, date, h
            var appointmentsR = await _context.Appointments
                        .Where(p => p.RoomId == roomId)
                        .Where(p => p.StartDate == startDate)
                        //.Where(p => p.EndDate == endDate)
                        .Where(p => p.Hidden == false)
                        .ToListAsync();

            if (appointmentsR.Count >= 1)
            {
                var room = await _context.Rooms.FirstOrDefaultAsync(m => m.Id == roomId);
                foundAppointment = $"{room.Name} este ocupata la data {startDate}.";
                return foundAppointment;
            }

            return foundAppointment;
        }
        #endregion
    }
}

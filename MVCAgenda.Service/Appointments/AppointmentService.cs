using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Core.Logging;
using MVCAgenda.Core.Pagination;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Logins;

namespace MVCAgenda.Service.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        #region Fields

        private string msg;
        private readonly AgendaContext _context;
        private readonly IWorkContext _workContext;
        private readonly ILoggerService _logger;

        #endregion

        /**************************************************************************************/

        #region Constructor

        public AppointmentService(AgendaContext context, ILoggerService logger, IWorkContext workContext)
        {
            _context = context;
            _logger = logger;
            _workContext = workContext;
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
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Appointments}, Action: {LogInfo.Create.ToString()}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return false;
            }
        }

        public async Task<Appointment> GetAsync(int Id)
        {
            try
            {
                return await _context.Appointments.FirstOrDefaultAsync(a => a.Id == Id);
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Appointments}, Action: {LogInfo.Read.ToString()}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return new Appointment();
            }
        }

        /// <summary>
        /// Get a list of appointments
        /// </summary>
        /// <param name="searchByAppointmentStartDate">Search appointments using start date</param>
        /// <param name="searchByAppointmentEndDate">Search appointments using end date</param>
        /// <param name="searchByRoom">Search appointments using room id</param>
        /// <param name="searchByMedic">Search appointments using medic id</param>
        /// <param name="searchByProcedure">Search appointments using a procedure</param>
        /// <param name="id">Search appointments using patient id</param>
        /// <param name="daily">Get daily appointments where start date = system.date</param>
        /// <param name="hidden">Get deleted appointments</param>
        /// <returns></returns>
        public async Task<List<Appointment>> GetFiltredListAsync(int pageIndex,
            DateTime? searchByAppointmentStartDate = null,
            DateTime? searchByAppointmentEndDate = null,
            int? searchByRoom = null,
            int? searchByMedic = null,
            string searchByProcedure = null,
            int? id = null,
            bool? made = null,
            bool? daily = null,
            bool? hidden = null)
        {
            try
            {
                var query = _context.Appointments.AsQueryable();

                if (searchByAppointmentStartDate != null && 
                    searchByAppointmentEndDate != null && 
                    searchByAppointmentStartDate < searchByAppointmentEndDate)
				{
                    query = query.Where(a => a.StartDate >= searchByAppointmentStartDate);
                    query = query.Where(a => a.EndDate <= searchByAppointmentEndDate);
                }

                if (searchByRoom != null)
                    query = query.Where(a => a.RoomId == searchByRoom);

                if (searchByMedic != null)
                    query = query.Where(a => a.MedicId == searchByMedic);

                if (searchByProcedure != null)
                    query = query.Where(a => a.Procedure.ToUpper().Contains(searchByProcedure.ToUpper()));

                //programrile unui pacient
                if (id != null)
                    query = query.Where(a => a.PatientId == id);

                //programrile neefectuatre
                if (made != null)
                    query = query.Where(a => a.Made == made);

                //programrile zilnice
                if (daily != null && id == null && 
                    searchByAppointmentStartDate == null && 
                    searchByAppointmentEndDate == null)
                    query = query.Where(a => a.StartDate.Date == DateTime.Now.Date)
                        .Where(a => a.StartDate >= DateTime.Now.AddMinutes(-60));

                //programrile sterse
                if (hidden != null)
                    query = query.Where(a => a.Hidden == hidden);

                query = query.OrderBy(a => a.StartDate);

                if (pageIndex != -1)
                    query = query.Skip((pageIndex - 1) * Constants.TotalItemsOnAPage).Take(Constants.TotalItemsOnAPage);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Appointments}, Action: {LogInfo.Read.ToString()}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return new List<Appointment>();
            }
        }

        public async Task<int> GetNumberOfFiltredAppointmentsAsync(
            string searchByName = null,
            string searchByPhoneNumber = null,
            string searchByEmail = null,
            DateTime? searchByAppointmentStartDate = null,
            DateTime? searchByAppointmentEndDate = null,
            int? searchByRoom = null,
            int? searchByMedic = null,
            string searchByProcedure = null,
            int? id = null,
            bool? made = null,
            bool? daily = null,
            bool? hidden = null)
        {
            try
            {
                var query = _context.Appointments.AsQueryable();

                if (searchByAppointmentStartDate != null &&
                    searchByAppointmentEndDate != null &&
                    searchByAppointmentStartDate < searchByAppointmentEndDate)
                {
                    query = query.Where(a => a.StartDate >= searchByAppointmentStartDate);
                    query = query.Where(a => a.EndDate <= searchByAppointmentEndDate);
                }

                if (searchByRoom != null)
                    query = query.Where(a => a.RoomId == searchByRoom);

                if (searchByMedic != null)
                    query = query.Where(a => a.MedicId == searchByMedic);

                if (searchByProcedure != null)
                    query = query.Where(a => a.Procedure.ToUpper().Contains(searchByProcedure.ToUpper()));

                //programrile unui pacient
                if (id != null)
                    query = query.Where(a => a.PatientId == id);

                //programrile neefectuatre
                if (made != null)
                    query = query.Where(a => a.Made == made);

                //programrile zilnice
                if (daily != null && id == null &&
                    searchByAppointmentStartDate == null &&
                    searchByAppointmentEndDate == null)
                    query = query.Where(a => a.StartDate.Date == DateTime.Now.Date)
                        .Where(a => a.StartDate >= DateTime.Now.AddMinutes(-60));

                //programrile sterse
                if (hidden != null)
                    query = query.Where(a => a.Hidden == hidden);

                var list = await query.ToListAsync();
                var listToCount = new List<ListItem>();
                foreach (var item in list)
                {
                    var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == item.PatientId);
                    listToCount.Add(new ListItem() 
                    {
                        FirstName = patient.FirstName,
                        PhoneNumber = patient.PhoneNumber,
                        Email = patient.Mail
                    });
                }
                var count = listToCount
                    .Where(p => !string.IsNullOrEmpty(searchByName) ? p.FirstName.ToUpper().Contains(searchByName.ToUpper()) : true)
                    .Where(p => !string.IsNullOrEmpty(searchByPhoneNumber) ? p.PhoneNumber.Contains(searchByPhoneNumber) : true)
                    .Where(p => !string.IsNullOrEmpty(searchByEmail) ? p.Email.Contains(searchByEmail) : true).Count();
                return count;
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Appointments}, Action: {LogInfo.Read.ToString()}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return 1;
            }
        }

        public async Task<AppointmentsPagination> GetAppointmentsPaginationAsync(int pageIndex,
            string searchByName = null,
            string searchByPhoneNumber = null,
            string searchByEmail = null,
            DateTime? searchByAppointmentStartDate = null,
            DateTime? searchByAppointmentEndDate = null,
            int? searchByRoom = null,
            int? searchByMedic = null,
            string searchByProcedure = null,
            int? id = null,
            bool? made = null,
            bool? daily = null,
            bool? hidden = null)
        {
            try
            {
                int count, totalPages;
                var appointmentsList = new List<AppointmentListItem>();
                IEnumerable<AppointmentListItem> appointments;
                

                var query = _context.Appointments.AsQueryable();

                if (searchByAppointmentStartDate != null &&
                    searchByAppointmentEndDate != null &&
                    searchByAppointmentStartDate < searchByAppointmentEndDate)
                {
                    query = query.Where(a => a.StartDate >= searchByAppointmentStartDate);
                    query = query.Where(a => a.EndDate <= searchByAppointmentEndDate);
                }

                if (searchByRoom != null)
                    query = query.Where(a => a.RoomId == searchByRoom);

                if (searchByMedic != null)
                    query = query.Where(a => a.MedicId == searchByMedic);

                if (searchByProcedure != null)
                    query = query.Where(a => a.Procedure.ToUpper().Contains(searchByProcedure.ToUpper()));

                //programrile unui pacient
                if (id != null)
                    query = query.Where(a => a.PatientId == id);

                //programrile neefectuatre
                if (made != null)
                    query = query.Where(a => a.Made == made);

                //programrile zilnice
                if (daily != null && id == null &&
                    searchByAppointmentStartDate == null &&
                    searchByAppointmentEndDate == null)
                    query = query.Where(a => a.StartDate.Date == DateTime.Now.Date)
                        .Where(a => a.StartDate >= DateTime.Now.AddMinutes(-60));

                //programrile sterse
                if (hidden != null)
                    query = query.Where(a => a.Hidden == hidden);

                query = query.OrderBy(a => a.StartDate);

                var queryAppointments = await query.ToListAsync();
                
                foreach (var appointment in queryAppointments)
                    appointmentsList.Add(await PrepereListItemAsync(appointment));

                appointmentsList = appointmentsList
                    .Where(p => !string.IsNullOrEmpty(searchByName) ? p.FirstName.ToUpper().Contains(searchByName.ToUpper()) : true)
                    .Where(p => !string.IsNullOrEmpty(searchByPhoneNumber) ? p.PhoneNumber.Contains(searchByPhoneNumber) : true)
                    .Where(p => !string.IsNullOrEmpty(searchByEmail) ? p.Mail.Contains(searchByEmail) : true).ToList();

                if (pageIndex != -1)
                    appointments = appointmentsList.Skip((pageIndex - 1) * Constants.TotalItemsOnAPage).Take(Constants.TotalItemsOnAPage);
                else
                    appointments = appointmentsList.ToList();

                return new AppointmentsPagination { AppointmentsCount = appointmentsList.Count, TotalPages = (int)Math.Ceiling(appointmentsList.Count / (double)Constants.TotalItemsOnAPage), Appointments = appointments };
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Appointments}, Action: {LogInfo.Read.ToString()}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return new AppointmentsPagination { AppointmentsCount = 0, Appointments = null };
            }
        }

        public async Task<bool> UpdateAsync(Appointment appointment)
        {
            try
            {
                var appointmentToBeEdited = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == appointment.Id);
                _context.Entry(appointmentToBeEdited).CurrentValues.SetValues(appointment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Appointments}, Action: {LogInfo.Edit.ToString()}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
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
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Appointments}, Action: {LogInfo.Hide.ToString()}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return false;
            }
        }

        public async Task<bool> UnHideAsync(int id)
        {
            try
            {
                var appointment = await _context.Appointments.FindAsync(id);
                appointment.Hidden = false;
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Appointments}, Action: {LogInfo.Hide.ToString()}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
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
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Appointments}, Action: {LogInfo.Delete}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
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

        public async Task<AppointmentListItem> PrepereListItemAsync(Appointment appointment)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == appointment.PatientId);
            var room = await _context.Rooms.FirstOrDefaultAsync(p => p.Id == appointment.RoomId);
            var medic = await _context.Medics.FirstOrDefaultAsync(p => p.Id == appointment.MedicId);

            return new AppointmentListItem()
            {
                Id = appointment.Id,
                EndDate = appointment.EndDate,
                StartDate = appointment.StartDate,
                Procedure = appointment.Procedure,
                Made = appointment.Made,

                PatientId = appointment.PatientId,
                FirstName = patient.FirstName,
                PhoneNumber = patient.PhoneNumber,
                Mail = patient.Mail,
                
                Medic = medic.Name,
                Room = room.Name,
                Hidden = appointment.Hidden
            };
        }

        public class ListItem
        {
            public string FirstName {  get; set; }
            public string PhoneNumber {  get; set; }
            public string Email {  get; set; }
        }

        #endregion
    }
}
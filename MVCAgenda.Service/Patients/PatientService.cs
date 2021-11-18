using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Data.DataBaseManager;
using System.Threading.Tasks;
using MVCAgenda.Service.Logins;
using MVCAgenda.Core.Logging;
using MVCAgenda.Core.Status;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Core;

namespace MVCAgenda.Service.Patients
{
    public class PatientService : IPatientService
    {
        #region Fields

        private string msg;
        private readonly AgendaContext _context; 
        private readonly IWorkContext _workContext;
        private readonly ILoggerService _logger;

        #endregion

        /***********************************************************************************/

        #region Constructor

        public PatientService(AgendaContext context, ILoggerService logger, IWorkContext workContext)
        {
            _context = context;
            _logger = logger;
            _workContext = workContext;
        }

        #endregion

        /***********************************************************************************/

        #region Create

        public async Task<bool> CreateAsync(Patient patient)
        {
            try
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();

                var currentSheetPatient = new PatientSheet() { PatientId = patient.Id};
                _context.Add(currentSheetPatient);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients}, Action: {LogInfo.Create}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return false;
            }
        }

        public async Task<int> CheckExistentPatientAsync(Patient patient)
        {
            try
            {
                //To do daca sa valideze doar dupa numar de telefon
                //Daca suna un pacient de pe numere diferite
                var newPatient = await _context.Patients
                    .Where(p => p.PhoneNumber == patient.PhoneNumber)
                    .Where(p => p.Hidden == false)
                    .FirstOrDefaultAsync();

                if (newPatient == null)
                {
                    newPatient = new Patient
                    {
                        FirstName = $"{patient.FirstName.Substring(0, 1).ToUpper()}{patient.FirstName.Substring(1, patient.FirstName.Length - 1).ToLower()}",
                        LastName = patient.LastName != null ? $"{patient.LastName.Substring(0, 1).ToUpper()}{patient.LastName.Substring(1, patient.LastName.Length - 1).ToLower()}" : null,
                        PhoneNumber = patient.PhoneNumber,
                        Mail = patient.Mail,
                        StatusCode = (int)PatientStatus.Patient,
                        Hidden = false
                    };

                    _context.Add(newPatient);
                    await _context.SaveChangesAsync();

                    var FisaPacientCurent = new PatientSheet() { PatientId = newPatient.Id};
                    _context.Add(FisaPacientCurent);
                    await _context.SaveChangesAsync();
                }

                return newPatient.Id;
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients}, Action: {LogInfo.Create}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return -1;
            }
        }

        #endregion

        /***********************************************************************************/

        #region Read

        public async Task<Patient> GetAsync(int Id)
        {
            try
            {
                return await _context.Patients.FirstOrDefaultAsync(p => p.Id == Id);
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients}, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return null;
            }
        }

        public async Task<List<Patient>> GetListAsync(int pageIndex,
            string searchByName = null,
            string searchByPhoneNumber = null,
            string searchByEmail = null,
            bool? includeBlackList = null,
            bool? isHidden = null)
        {
            try
            {
                var query = _context.Patients.AsQueryable();

                if (isHidden != null)
                    query = query.Where(p => p.Hidden == isHidden);

                if (includeBlackList == true)
                    query = query.Where(p => p.StatusCode == (int)PatientStatus.Blacklist);

                if (searchByName != null)
                    query = query.Where(p => p.FirstName.ToUpper().Contains(searchByName.ToUpper()));

                if (searchByPhoneNumber != null)
                    query = query.Where(p => p.PhoneNumber.Contains(searchByPhoneNumber));

                if (searchByEmail != null)
                    query = query.Where(p => p.Mail.ToUpper().Contains(searchByEmail.ToUpper()));

                query = query.OrderBy(f => f.FirstName);

                if (pageIndex != -1)
                    query = query.Skip((pageIndex - 1) * Constants.TotalItemsOnAPage).Take(Constants.TotalItemsOnAPage);
                
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients}, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return null;
            }
        }

        public async Task<int> GetPatientsNumberAsync(
            string searchByName = null,
            string searchByPhoneNumber = null,
            string searchByEmail = null,
            bool? includeBlackList = null,
            bool? isHidden = null)
        {
            try
            {
                var query = _context.Patients.AsQueryable();

                if (isHidden != null)
                    query = query.Where(p => p.Hidden == isHidden);

                if (includeBlackList != null)
                    query = query.Where(p => p.StatusCode == (int)PatientStatus.Blacklist);

                if (searchByName != null)
                    query = query.Where(p => p.FirstName.ToUpper().Contains(searchByName.ToUpper()));

                if (searchByPhoneNumber != null)
                    query = query.Where(p => p.PhoneNumber.Contains(searchByPhoneNumber));

                if (searchByEmail != null)
                    query = query.Where(p => p.Mail.ToUpper().Contains(searchByEmail.ToUpper()));

                return await query.CountAsync();
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients}, Action: {LogInfo.Read}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return 0;
            }
        }

        #endregion

        /***********************************************************************************/

        #region Update

        public async Task<bool> UpdateAsync(Patient patient)
        {
            try
            {
                var patientToBeEdited = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patient.Id);
                _context.Entry(patientToBeEdited).CurrentValues.SetValues(patient);
                //_context.Update(patient);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients}, Action: {LogInfo.Edit}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return false;
            }
        }

        #endregion

        /***********************************************************************************/

        #region Delete

        public async Task<bool> HideAsync(int id)
        {
            try
            {
                var patient = await _context.Patients.FindAsync(id);
                patient.Hidden = true;
                _context.Patients.Update(patient);

                var appointments = _context.Appointments.Where(a => a.PatientId == patient.Id);
                await appointments.ForEachAsync(x => x.Hidden = true);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients}, Action: {LogInfo.Hide}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return false;
            }
        }

        public async Task<bool> UnHideAsync(int id)
        {
            try
            {
                var patient = await _context.Patients.FindAsync(id);
                patient.Hidden = false;
                _context.Patients.Update(patient);

                var appointments = _context.Appointments.Where(a => a.PatientId == patient.Id);
                await appointments.ForEachAsync(x => x.Hidden = false);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients}, Action: {LogInfo.UnHide}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var patient = await _context.Patients.FindAsync(id);
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                msg = $"User: {(await _workContext.GetCurrentUserAsync()).Identity.Name}, Table:{LogTable.Patients}, Action: {LogInfo.Delete}";
                await _logger.CreateAsync(msg, ex.Message, null, LogLevel.Error);
                return false;
            }
        }

        #endregion
    }
}
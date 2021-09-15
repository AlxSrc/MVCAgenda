using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Data.DataBaseManager;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Patients
{
    public class PatientServices : IPatientServices
    {
        #region Fields
        private readonly AgendaContext _context;
        #endregion
        /***********************************************************************************/
        #region Constructor
        public PatientServices(AgendaContext context)
        {
            _context = context;
        }
        #endregion
        /***********************************************************************************/
        #region Create
        public async Task<bool> CreateAsync(Patient patient)
        {
            try
            {
                var currentSheetPatient = new PatientSheet();
                _context.Add(currentSheetPatient);
                await _context.SaveChangesAsync();

                patient.PatientSheetId = currentSheetPatient.Id;
                _context.Add(patient);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
        //de sters
        public async Task<int> CheckExistentPatientAsync(Patient patient)
        {
            try
            {
                //To do daca sa valideze doar dupa numar de telefon
                //Daca suna un pacient de pe numere diferite
                var patients = await _context.Patients
                    .Where(p => p.FirstName == patient.FirstName)
                    .Where(p => p.SecondName == patient.SecondName)
                    .Where(p => p.PhonNumber == patient.PhonNumber)
                    .Where(p => p.Mail == patient.Mail)
                    .Where(p => p.Hidden == false)
                    .ToListAsync();

                var newPatient = patients.FirstOrDefault();

                if (newPatient == null)
                {
                    newPatient = new Patient
                    {
                        FirstName = $"{patient.FirstName.Substring(0, 1).ToUpper()}{patient.FirstName.Substring(1, patient.FirstName.Length - 1).ToLower()}",
                        SecondName = patient.SecondName != null ? $"{patient.SecondName.Substring(0, 1).ToUpper()}{patient.SecondName.Substring(1, patient.SecondName.Length - 1).ToLower()}" : null,
                        PhonNumber = patient.PhonNumber,
                        Mail = patient.Mail,
                        Blacklist = false,
                        Hidden = false
                    };

                    var FisaPacientCurent = new PatientSheet();
                    _context.Add(FisaPacientCurent);
                    await _context.SaveChangesAsync();

                    newPatient.PatientSheetId = FisaPacientCurent.Id;
                    _context.Add(newPatient);
                    await _context.SaveChangesAsync();
                }

                return newPatient.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion
        /***********************************************************************************/
        #region Read
        public async Task<Patient> GetAsync(int Id, bool GetPatientByPatientSheetId = false)
        {
            if(GetPatientByPatientSheetId == false)
                return await _context.Patients.FirstOrDefaultAsync(p => p.Id == Id);
            else
                return await _context.Patients.FirstOrDefaultAsync(p => p.PatientSheetId == Id);
        }
        public async Task<List<Patient>> GetListAsync(string searchByName, string searchByPhoneNumber, string searchByEmail, bool includeBlackList, bool isHidden)
        {
            return await (
                    _context.Patients

                        .Where(h => isHidden == true ? h.Hidden == true : h.Hidden == false)
                        .Where(b => includeBlackList == true ? b.Blacklist == true : true)
                        .Where(p => !string.IsNullOrEmpty(searchByName) ? p.FirstName.ToUpper().Contains(searchByName.ToUpper()) : true)
                        .Where(p => !string.IsNullOrEmpty(searchByPhoneNumber) ? p.PhonNumber.Contains(searchByPhoneNumber) : true)
                        .Where(p => !string.IsNullOrEmpty(searchByEmail) ? p.Mail.ToUpper().Contains(searchByEmail.ToUpper()) : true)

                        .OrderBy(f => f.FirstName)

                    ).ToListAsync();
        }
        #endregion
        /***********************************************************************************/
        #region Update
        public async Task<bool> UpdateAsync(Patient patient)
        {
            try
            {
                _context.Update(patient);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
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
            catch
            {
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
            catch
            {
                return false;
            }
            
        }
        #endregion
    }
}

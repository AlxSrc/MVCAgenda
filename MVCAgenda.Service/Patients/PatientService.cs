﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Data.DataBaseManager;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Patients
{
    public class PatientService : IPatientService
    {
        #region Fields
        private readonly AgendaContext _context;
        #endregion
        /***********************************************************************************/
        #region Constructor
        public PatientService(AgendaContext context)
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
                var newPatient = await _context.Patients
                    .Where(p => p.FirstName == patient.FirstName)
                    .Where(p => p.LastName == patient.LastName)
                    .Where(p => p.PhoneNumber == patient.PhoneNumber)
                    .Where(p => p.Mail == patient.Mail)
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

        public async Task<List<Patient>> GetListAsync(string searchByName = null,
            string searchByPhoneNumber = null,
            string searchByEmail = null,
            bool? includeBlackList = false,
            bool? isHidden = false)
        {
            var query = _context.Patients.AsQueryable();

            if (isHidden != null)
                query = query.Where(p => p.Hidden == isHidden);

            if (includeBlackList != null)
                query = query.Where(p => p.Blacklist == includeBlackList);

            if (searchByName != null)
                query = query.Where(p => p.FirstName.ToUpper().Contains(searchByName.ToUpper()));

            if (searchByPhoneNumber != null)
                query = query.Where(p => p.PhoneNumber.Contains(searchByPhoneNumber));

            if (searchByEmail != null)
                query = query.Where(p => p.Mail.ToUpper().Contains(searchByEmail.ToUpper()));

            return await query.ToListAsync();
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

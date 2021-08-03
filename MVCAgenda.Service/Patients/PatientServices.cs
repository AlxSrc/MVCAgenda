using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Data.DataBaseManager;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace MVCAgenda.Service.Patients
{
    public class PatientServices : IPatientServices
    {
        private readonly AgendaContext _context;

        public PatientServices(AgendaContext context)
        {
            _context = context;
        }

        public async Task<string> CreatePatientAsync(Core.Domain.Patient PatientModel)
        {
            try
            {
                string msg;

                var patients = await _context.Patient
                                .Where(p => p.FirstName.Contains(PatientModel.FirstName))
                                .Where(p => p.SecondName.Contains(PatientModel.SecondName))
                                .Where(p => p.PhonNumber.Contains(PatientModel.PhonNumber))
                                .ToListAsync();

                if (patients.Count >= 1)
                {
                    msg = $"Exista un pacient cu numele: {PatientModel.FirstName}, prenumele: {PatientModel.PhonNumber} si numarul de telefon: {PatientModel.PhonNumber}";
                    return msg;
                }
                else
                {
                    SheetPatient SheetCurrentPatient = new SheetPatient();
                    _context.Add(SheetCurrentPatient);
                    await _context.SaveChangesAsync();

                    int lastID = _context.SheetPatient.Count();
                    PatientModel.SheetPatientId = lastID;
                    _context.Add(PatientModel);
                    await _context.SaveChangesAsync();

                    return "Ok";
                }
            }
            catch(Exception ex)
            {
                return "error "+ ex.Message;
            }
        }

        public Task<bool> EditPatientAsync(Core.Domain.Patient PatientModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            try
            {
                var patient = await _context.Patient.FindAsync(id);
                _context.Patient.Remove(patient);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> HidePatientAsync(int id)
        {
            try
            {
                var patient = await _context.Patient.FindAsync(id);
                patient.Visible = 0;
                _context.Patient.Update(patient);
                //_context.Pacient.Remove(pacient);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<List<Core.Domain.Patient>> GetPatientAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Core.Domain.Patient> GetPatientByIdAsync(int Id)
        {
            throw new System.NotImplementedException();
        }
    }
}

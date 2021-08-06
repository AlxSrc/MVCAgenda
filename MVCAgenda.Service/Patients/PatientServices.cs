using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.MVCAgendaManagement;
using MVCAgenda.Core.ViewModels;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Factories;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace MVCAgenda.Service.Patients
{
    public class PatientServices : IPatientServices
    {
        private readonly AgendaContext _context;

        private readonly IAgendaViewsFactory _agendaViewsFactory;
        public PatientServices(AgendaContext context, IAgendaViewsFactory agendaViewsFactory)
        {
            _context = context;
            _agendaViewsFactory = agendaViewsFactory;
        }

        public async Task<string> CreatePatientAsync(PatientViewModel PatientModel)
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
                    SheetPatient CurrentSheetPatient = new SheetPatient();
                    _context.Add(CurrentSheetPatient);
                    await _context.SaveChangesAsync();

                    int lastID = _context.SheetPatient.Count();

                    _context.Add(new Patient()
                    {
                        SheetPatientId = lastID,
                        FirstName = PatientModel.FirstName,
                        SecondName = PatientModel.SecondName,
                        PhonNumber = PatientModel.PhonNumber,
                        Mail = PatientModel.Mail,
                        Blacklist = int.Parse(PatientModel.Blacklist),
                    });
                    await _context.SaveChangesAsync();

                    return "Ok";
                }
            }
            catch(Exception ex)
            {
                return "error "+ ex.Message;
            }
        }

        public async Task<string> EditPatientAsync(PatientViewModel PatientModel)
        {
            try
            {
                _context.Update(new Patient() 
                { 
                    Id= PatientModel.Id,
                    SheetPatientId = PatientModel.SheetPatientId,
                    FirstName = PatientModel.FirstName,
                    SecondName = PatientModel.SecondName,
                    PhonNumber = PatientModel.PhonNumber,
                    Mail = PatientModel.Mail,
                    Blacklist = int.Parse(PatientModel.Blacklist),
                    Visible = PatientModel.Visible? 1:0
                });
                await _context.SaveChangesAsync();
                return "Succes";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Patient.Any(p => p.Id == PatientModel.Id))
                {
                    return "Error, Not Found";
                }
                else
                {
                    throw;
                }
            }
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

        public async Task<string> HidePatientAsync(int id)
        {
            try
            {
                var patient = await _context.Patient.FindAsync(id);
                patient.Visible = 0;
                _context.Patient.Update(patient);
                await _context.SaveChangesAsync();
                return "";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<MVCAgendaViewsManager> GetPatientAsync(string SearchByName, string SearchByPhoneNumber, string SearchByEmail, bool includeBlackList, bool isDeleted)
        {
            List<PatientViewModel> patientsList = new List<PatientViewModel>();
            var model = new MVCAgendaViewsManager{ PatientsList = patientsList };
            try
            {
                IQueryable<Patient> query = _context.Patient;

                if (isDeleted)
                    query = query.Where(p => p.Visible == 0);
                else
                    query = query.Where(p => p.Visible == 1);

                if (includeBlackList)
                    query = query.Where(p => p.Blacklist == 1);

                if (!string.IsNullOrEmpty(SearchByName))
                    query = query.Where(p => p.FirstName.Contains(SearchByName));

                if (!string.IsNullOrEmpty(SearchByPhoneNumber))
                    query = query.Where(p => p.PhonNumber.Contains(SearchByPhoneNumber));

                if (!string.IsNullOrEmpty(SearchByEmail))
                    query = query.Where(p => p.Mail.Contains(SearchByEmail));

                var patients = await query.ToListAsync(); // aici aducem datele despre pacienti prin sintaxa SQL

                var patientsModel = patients // lista de pacient adusa
                    .Select(patient => _agendaViewsFactory.PreperePatientViewModel(patient))
                    .ToList();

                model.PatientsList = patientsModel;
                return model;
            }
            catch
            {
                return model;
            }
        }

        public async Task<Patient> GetPatientByIdAsync(int Id)
        {
            Patient patient = new Patient();
            Patient emptyPatient = new Patient();
            try
            {
                if (Id == null)
                {
                    return emptyPatient;
                }

                patient = await _context.Patient.FirstOrDefaultAsync(m => m.Id == Id);

                if (patient == null)
                {
                    return emptyPatient;
                }

                return patient;
            }
            catch
            {
                return emptyPatient;
            }
        }
        public async Task<PatientViewModel> GetPatientViewModelByIdAsync(Patient patient)
        {
            PatientViewModel patientViewModel = new PatientViewModel();
            PatientViewModel emptyPatientViewModel = new PatientViewModel();
            try
            {
                return _agendaViewsFactory.PreperePatientViewModel(patient);
            }
            catch
            {
                return emptyPatientViewModel;
            }
        }
    }
}

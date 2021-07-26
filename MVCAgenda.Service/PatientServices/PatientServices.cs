using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Service.PatientServices
{
    public class PatientServices : IPatientServices
    {
        private readonly AgendaContext _context;

        public PatientServices(AgendaContext context)
        {
            _context = context;
        }

        public async Task<string> CreatePatientAsync(PatientDto PatientModel)
        {
            try
            {
                string msg;

                var patients = await _context.PatientDto
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
                    SheetPatientDto SheetCurrentPatient = new SheetPatientDto();
                    _context.Add(SheetCurrentPatient);
                    await _context.SaveChangesAsync();

                    int lastID = _context.SheetPatientDto.Count();
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

        public Task<bool> EditPatientAsync(PatientDto PatientModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeletePatientAsync(PatientDto PatientModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> HidePatientAsync(PatientDto PatientModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<PatientDto>> GetPatientAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<PatientDto> GetPatientByIdAsync(int Id)
        {
            throw new System.NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Data.DataBaseManager;

namespace MVCAgenda.Service.Consultations
{
    public class ConsultationServices : IConsultationServices
    {
        #region Fields
        private readonly AgendaContext _context;
        #endregion
        /**************************************************************************************/
        #region Constructor
        public ConsultationServices(AgendaContext context)
        {
            _context = context;
        }
        #endregion
        /**************************************************************************************/
        #region Methods
        public async Task<bool> CreateAsync(Consultation consultation)
        {
            try
            {
                _context.Add(consultation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public async Task<Consultation> GetAsync(int id)
        {
            var consultation = await _context.Consultations.FirstOrDefaultAsync(m => m.Id == id);
            return consultation;
        }
        public async Task<List<Consultation>> GetListAsync(int id)
        {
            var query = _context.Consultations.Where(p => p.SheetPatientId == id);
            return await query.OrderByDescending(c => c.CreationDate).Where(c => c.Hidden == false).ToListAsync();
        }
       
        public async Task<bool> UpdateAsync(Consultation consultation)
        {
            try
            {
                _context.Update(consultation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public async Task<bool> HideAsync(int id)
        {
            try
            {
                var consultation = await _context.Consultations.FindAsync(id);
                consultation.Hidden = true;
                _context.Consultations.Update(consultation);
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
                _context.Consultations.Remove(await _context.Consultations.FindAsync(id));
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

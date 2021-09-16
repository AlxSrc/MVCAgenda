using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Data.DataBaseManager;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Medics
{
    public class MedicService : IMedicService
    {
        #region Fields
        private readonly AgendaContext _context;
        #endregion
        /**************************************************************************************/
        #region Constructor
        public MedicService(AgendaContext context)
        {
            _context = context;
        }
        #endregion
        /**************************************************************************************/
        #region Methods
        public async Task<bool> CreateAsync(Medic medic)
        {
            try
            {
                _context.Add(medic);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public async Task<Medic> GetAsync(int id)
        {
            return await _context.Medics.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<List<Medic>> GetListAsync()
        {
            return await _context.Medics.OrderBy(m => m.Name).Where(m => m.Hidden == false).ToListAsync();
        }
        
        public async Task<bool> UpdateAsync(Medic medic)
        {
            try
            {
                _context.Update(medic);
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
                var medic = await _context.Medics.FindAsync(id);
                medic.Hidden = true;
                _context.Medics.Update(medic);
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
                _context.Medics.Remove(await _context.Medics.FindAsync(id));
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

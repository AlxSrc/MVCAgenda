using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Consultations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Service.SheetPatients
{
    public class PatientSheetServices : IPatientSheetServices
    {
        #region Fields
        private readonly AgendaContext _context;
        #endregion
        /**************************************************************************************/
        #region Constructor
        public PatientSheetServices(AgendaContext context)
        {
            _context = context;
        }
        #endregion
        /**************************************************************************************/
        #region Methods
        public async Task<PatientSheet> GetAsync(int Id)
        {
            return await _context.PatientsSheet.FirstOrDefaultAsync(m => m.Id == Id);
        }
       
        public async Task<bool> UpdateAsync(PatientSheet patientSheet)
        {
            try
            {
                _context.Update(patientSheet);
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

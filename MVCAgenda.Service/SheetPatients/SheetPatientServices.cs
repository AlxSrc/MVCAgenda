using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.ViewModels;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Factories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAgenda.Service.SheetPatients
{
    public class SheetPatientServices : ISheetPatientServices
    {
        private readonly AgendaContext _context;
        private readonly IAgendaViewsFactory _agendaViewsFactory;

        public SheetPatientServices(AgendaContext context, IAgendaViewsFactory agendaViewsFactory)
        {
            _context = context;
            _agendaViewsFactory = agendaViewsFactory;
        }

        public Task<string> EditSheetPatientAsync(SheetPatient SheetPatientModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SheetPatient> GetSheetPatientByIdAsync(int Id)
        {
            SheetPatient sheetPatient = new SheetPatient();
            if (Id == null)
            {
                return sheetPatient;
            }

            sheetPatient = await _context.SheetPatient.FirstOrDefaultAsync(m => m.Id == Id);

            if (sheetPatient == null)
            {
                return sheetPatient;
            }

            return sheetPatient;
        }

        public async Task<SheetPatientViewModel> GetSheetPatientViewModelByIdAsync(int Id)
        {
            List<Consultation> consultationsList = new List<Consultation>();
            SheetPatientViewModel emptySheetPatientModel = new SheetPatientViewModel() { Consultations = consultationsList };
            SheetPatientViewModel sheetPatientModel = new SheetPatientViewModel() { Consultations = consultationsList };
            try
            {
                if (Id == null)
                {
                    return emptySheetPatientModel;
                }

                var sheetPatient = await _context.SheetPatient.FirstOrDefaultAsync(m => m.Id == Id);

                if (sheetPatient == null)
                {
                    return emptySheetPatientModel;
                }

                IQueryable<Consultation> query = _context.Consultation;
                query = query.Where(p => p.SheetPatientId == Id);

                var consultations = await query.OrderByDescending(c => c.CreationDate).ToListAsync();

                sheetPatientModel = await _agendaViewsFactory.PrepereSheetPatientViewModel(sheetPatient, consultations);

                return sheetPatientModel;
            }
            catch
            {
                return emptySheetPatientModel;
            }
            
        }
    }
}

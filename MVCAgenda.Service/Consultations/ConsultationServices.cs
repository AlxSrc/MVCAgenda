using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.ViewModels;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAgenda.Service.Consultations
{
    public class ConsultationServices : IConsultationServices
    {
        #region Services
        private readonly AgendaContext _context;
        private readonly IAgendaViewsFactory _agendaViewsFactory; 
        #endregion
        /**************************************************************************************/
        #region Fields
        private static DateTime ActualDateTime = DateTime.Now;

        private string DayTime = ActualDateTime.ToString("yyyy-MM-dd");
        private string _dataCreeareConsultatie = ActualDateTime.ToString("U");
        #endregion

        public ConsultationServices(AgendaContext context, IAgendaViewsFactory agendaViewsFactory)
        {
            _context = context;
            _agendaViewsFactory = agendaViewsFactory;
        }

        #region Create
        public async Task<bool> CreateConsultationAsync(ConsultationViewModel consultation)
        {
            try
            {
                _context.Add(new Consultation()
                {
                    SheetPatientId = consultation.SheetPatientId,
                    CreationDate = consultation.CreationDate,
                    Symptoms = consultation.Symptoms,
                    Diagnostic = consultation.Diagnostic,
                    Prescriptions = consultation.Prescriptions
                });
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        #endregion
        /**************************************************************************************/
        #region Get
        public async Task<ConsultationViewModel> GetConsultationAsync(int id)
        {
            try
            {
                if (id == null)
                {
                    return new ConsultationViewModel();
                }

                var consultation = await _context.Consultation.FirstOrDefaultAsync(m => m.Id == id);
                if (consultation == null)
                {
                    return new ConsultationViewModel();
                }

                return await _agendaViewsFactory.PrepereConsultationViewModel(consultation);
            }
            catch
            {
                return new ConsultationViewModel();
            }
        }
        public async Task<List<ConsultationViewModel>> GetConsultationsAsync(int id)
        {
            try
            {
                var consultationViewModel = new List<ConsultationViewModel>();
                IQueryable<Consultation> query = _context.Consultation;
                query = query.Where(p => p.SheetPatientId == id);

                var consultations = await query.OrderByDescending(c => c.CreationDate).Where(c => c.Hidden == false).ToListAsync();

                foreach (var consul in consultations)
                    consultationViewModel.Add(await _agendaViewsFactory.PrepereConsultationViewModel(consul));

                return consultationViewModel;
            }
            catch
            {
                return new List<ConsultationViewModel>();
            }
        }
        #endregion
        /**************************************************************************************/
        #region Edit
        public async Task<bool> EditConsultationAsync(ConsultationViewModel consultationModel)
        {
            var consultation = new Consultation();
            try
            {
                consultation.Id = consultationModel.Id;
                consultation.SheetPatientId = consultationModel.SheetPatientId;
                consultation.CreationDate = consultationModel.CreationDate;
                consultation.Prescriptions = consultationModel.Prescriptions;
                consultation.Symptoms = consultationModel.Symptoms;
                consultation.Diagnostic = consultationModel.Diagnostic;

                if (_context.Consultation.Any(e => e.Id == consultationModel.Id))
                {
                    _context.Update(consultation);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        /**************************************************************************************/
        #region Hide
        public async Task<bool> HideConsultationAsync(int id)
        {
            try
            {
                var consultation = await _context.Consultation.FindAsync(id);
                consultation.Hidden = true;
                _context.Consultation.Update(consultation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        /**************************************************************************************/
        #region Delete
        public async Task<bool> DeleteConsultationAsync(int id)
        {
            try
            {
                var consultation = await _context.Consultation.FindAsync(id);
                _context.Consultation.Remove(consultation);
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

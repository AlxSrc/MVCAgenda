using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.ViewModels;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Consultations;
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
        private readonly IConsultationServices _consultationServices; 

        public SheetPatientServices(AgendaContext context, IAgendaViewsFactory agendaViewsFactory, IConsultationServices consultationServices)
        {
            _context = context;
            _agendaViewsFactory = agendaViewsFactory;
            _consultationServices = consultationServices;
        }

        public async Task<string> EditSheetPatientAsync(SheetPatientViewModel SheetPatientModel)
        {
            try
            {
                var sheetPatient = new SheetPatient()
                {
                    Id = SheetPatientModel.Id,
                    AntecedentsH = SheetPatientModel.AntecedentsH,
                    AntecedentsP = SheetPatientModel.AntecedentsP,
                    CNP = SheetPatientModel.CNP,
                    DateOfBirth = SheetPatientModel.DateOfBirth,
                    Street = SheetPatientModel.Street,
                    Gender = int.Parse(SheetPatientModel.Gender),
                    Town = SheetPatientModel.Town,
                    PhysicalExamination = SheetPatientModel.PhysicalExamination
                };

                _context.Update(sheetPatient);
                await _context.SaveChangesAsync();
                return "";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.SheetPatient.Any(m => m.Id == SheetPatientModel.Id))
                {
                    return "error";
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<SheetPatientViewModel> GetSheetPatientByIdAsync(int Id)
        {
            if (Id == null)
            {
                return new SheetPatientViewModel();
            }

            var sheetPatient = await _context.SheetPatient.FirstOrDefaultAsync(m => m.Id == Id);

            if (sheetPatient == null)
            {
                return new SheetPatientViewModel();
            }
            string gender;
            if (sheetPatient.Gender == 1)
                gender = "1";
            else
                gender = "0";

            return new SheetPatientViewModel()
            {
                Id = sheetPatient.Id,
                AntecedentsH = sheetPatient.AntecedentsH,
                AntecedentsP = sheetPatient.AntecedentsP,
                CNP = sheetPatient.CNP,
                DateOfBirth = sheetPatient.DateOfBirth,
                Street = sheetPatient.Street,
                Town = sheetPatient.Town,
                Gender = gender,
                PhysicalExamination = sheetPatient.PhysicalExamination
            };
        }

        public async Task<SheetPatientViewModel> GetSheetPatientViewModelByIdAsync(int Id)
        {
            try
            {
                if (Id == null)
                {
                    return new SheetPatientViewModel() { Consultations = new List<ConsultationViewModel>() };
                }

                var sheetPatient = await _context.SheetPatient.FirstOrDefaultAsync(m => m.Id == Id);

                if (sheetPatient == null)
                {
                    return new SheetPatientViewModel() { Consultations = new List<ConsultationViewModel>() };
                }
                
                var consultations = await _consultationServices.GetConsultationsAsync(Id);
                var patients = await _context.Patient.ToListAsync();
                var patient = patients.FirstOrDefault(p => p.SheetPatientId == Id);

                return await _agendaViewsFactory.PrepereSheetPatientViewModel(sheetPatient, patient, consultations);
            }
            catch
            {
                return new SheetPatientViewModel() { Consultations = new List<ConsultationViewModel>() };
            }
            
        }
    }
}

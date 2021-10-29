using MVCAgenda.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Service.PatientsSheet
{
    public interface IPatientSheetService
    {
        Task<bool> CreateAsync(PatientSheet patientSheet);
        Task<PatientSheet> GetAsync(int Id);
        Task<List<PatientSheet>> GetListAsync();

        Task<bool> UpdateAsync(PatientSheet patientSheet);
    }
}
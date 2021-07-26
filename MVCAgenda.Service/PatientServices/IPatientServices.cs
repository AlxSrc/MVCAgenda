using MVCAgenda.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAgenda.Service.PatientServices
{
    public interface IPatientServices
    {
        Task<string> CreatePatientAsync(PatientDto PatientModel);
        Task<bool> EditPatientAsync(PatientDto PatientModel);
        Task<bool> DeletePatientAsync(PatientDto PatientModel);
        Task<bool> HidePatientAsync(PatientDto PatientModel);
        Task<List<PatientDto>> GetPatientAsync();
        Task<PatientDto> GetPatientByIdAsync(int Id);
    }
}

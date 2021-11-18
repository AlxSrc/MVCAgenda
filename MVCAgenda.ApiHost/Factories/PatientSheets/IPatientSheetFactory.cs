using MVCAgenda.ApiHost.DTOs.PatientSheets;
using System.Threading.Tasks;

namespace MVCAgenda.ApiHost.Factories.PatientSheets
{
    public interface IPatientSheetFactory
    {
        Task<PatientSheetDto> PrepereDtoAsync(int id);
    }
}

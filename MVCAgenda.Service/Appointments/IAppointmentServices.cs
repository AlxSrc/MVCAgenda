using System.Collections.Generic;
using System.Threading.Tasks;
using MVCAgenda.Core.Domain;

namespace MVCAgenda.Service.Appointments
{
    public interface IAppointmentServices
    {
        Task<bool> CreateAsync(Appointment appointment);
        
        Task<Appointment> GetAsync(int Id);
        Task<List<Appointment>> GetListAsync(string SearchByAppointmentHour, string SearchByAppointmentDate, int SearchByRoom, int SearchByMedic, string SearchByProcedure, int Id, bool daily, bool Hiden);

        Task<bool> UpdateAsync(Appointment appointment);

        Task<bool> HideAsync(int id);
        Task<bool> DeleteAsync(int id);
        
        Task<string> SearchAppointmentAsync(int MedicId, int RoomId, string AppointmentDate, string AppointmentHour);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCAgenda.Core.Domain;

namespace MVCAgenda.Service.Appointments
{
    public interface IAppointmentService
    {
        Task<bool> CreateAsync(Appointment appointment);
        
        Task<Appointment> GetAsync(int Id);

        Task<List<Appointment>> GetFiltredListAsync(DateTime? searchByAppointmentStartDate = null,
            DateTime? searchByAppointmentEndDate = null,
            int? searchByRoom = null,
            int? searchByMedic = null,
            string searchByProcedure = null,
            int? id = null,
            bool? Daily = null,
            bool? Hidden = null);

        Task<bool> UpdateAsync(Appointment appointment);

        Task<bool> HideAsync(int id);
        Task<bool> DeleteAsync(int id);
        
        Task<string> SearchAppointmentAsync(int MedicId, int RoomId, DateTime startDate, DateTime endDate);
    }
}

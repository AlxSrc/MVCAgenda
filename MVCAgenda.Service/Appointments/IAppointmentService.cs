using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Pagination;

namespace MVCAgenda.Service.Appointments
{
    public interface IAppointmentService
    {
        Task<bool> CreateAsync(Appointment appointment);

        Task<Appointment> GetAsync(int Id);

        Task<List<Appointment>> GetAppointmentListAsync();

        Task<List<Appointment>> GetFiltredListAsync(int pageIndex,
            DateTime? searchByAppointmentStartDate = null,
            DateTime? searchByAppointmentEndDate = null,
            int? searchByRoom = null,
            int? searchByMedic = null,
            string searchByProcedure = null,
            int? id = null,
            bool? made = null,
            bool? daily = null,
            bool? hidden = null,
            bool? privateAppointment = null);

        Task<int> GetNumberOfFiltredAppointmentsAsync(
            string searchByName = null,
            string searchByPhoneNumber = null,
            string searchByEmail = null,
            DateTime? searchByAppointmentStartDate = null,
            DateTime? searchByAppointmentEndDate = null,
            int? searchByRoom = null,
            int? searchByMedic = null,
            string searchByProcedure = null,
            int? id = null,
            bool? made = null,
            bool? daily = null,
            bool? hidden = null,
            bool? privateAppointment = null);

        Task<AppointmentsPagination> GetAppointmentsPaginationAsync(
            int pageIndex,
            string searchByName = null,
            string searchByPhoneNumber = null,
            string searchByEmail = null,
            DateTime? searchByAppointmentStartDate = null,
            DateTime? searchByAppointmentEndDate = null,
            int? searchByRoom = null,
            int? searchByMedic = null,
            string searchByProcedure = null,
            int? id = null,
            bool? made = null,
            bool? Daily = null,
            bool? hidden = null,
            bool? privateAppointment = null);

        Task<bool> UpdateAsync(Appointment appointment);

        Task<bool> HideAsync(int id);
        Task<bool> UnHideAsync(int id);
        Task<bool> DeleteAsync(int id);

        Task<string> SearchAppointmentAsync(int MedicId, int RoomId, DateTime startDate, DateTime endDate);
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.MVCAgendaManagement;
using MVCAgenda.Core.ViewModels;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Factories;

namespace MVCAgenda.Service.Appointments
{
    public class AppointmentServices : IAppointmentServices
    {
        #region Services
        private readonly AgendaContext _context;
        private readonly IAgendaViewsFactory _agendaViewsFactory;
        #endregion
        /**************************************************************************************/
        #region Fields
        private string DayTime = DateTime.Now.ToString("yyyy-MM-dd");
        #endregion

        public AppointmentServices(AgendaContext context, IAgendaViewsFactory agendaViewsFactory)
        {
            _context = context;
            _agendaViewsFactory = agendaViewsFactory;
        }

        #region Create

        public async Task<string> CreateAppointmentAsync(AppointmentViewModel appointment)
        {
            try
            {
                string message = searchAppointment(appointment.MedicId, appointment.RoomId, appointment.AppointmentDate, appointment.AppointmentHour);
                if (message != "Clear")
                {
                    return message;
                }
                else
                {
                    if (appointment.PatientId > 0)
                    {
                        Patient patient = await _context.Patient.FindAsync(appointment.PatientId);
                        if (patient == null)
                        {
                            return "Error. Not found.";
                        }

                        var newAppointment = new Appointment
                        {
                            PatientId = patient.Id,
                            MedicId = appointment.MedicId,
                            RoomId = appointment.RoomId,
                            AppointmentDate = appointment.AppointmentDate,
                            AppointmentHour = appointment.AppointmentHour,
                            Procedure = appointment.Procedure,
                            Made = appointment.Made,
                            ResponsibleForAppointment = appointment.ResponsibleForAppointment,
                            AppointmentCreationDate = appointment.AppointmentCreationDate,
                            Comments = appointment.Comments,
                            Hidden = appointment.Hidden
                        };

                        _context.Add(newAppointment);
                        await _context.SaveChangesAsync();

                        return "Ok";
                    }
                    else
                    {
                        //To do daca sa valideze doar dupa numar de telefon
                        //Daca suna un pacient de pe numere diferite
                        var patients = await _context.Patient
                            .Where(p => p.FirstName == appointment.FirstName)
                            .Where(p => p.SecondName == appointment.SecondName)
                            .Where(p => p.PhonNumber == appointment.PhonNumber)
                            .Where(p => p.Mail == appointment.Mail)
                            .ToListAsync();

                        if (patients.Count > 1)
                            return "Eror. Status cod 500";

                        var newPatient = patients.FirstOrDefault();

                        if (newPatient == null)
                        {
                            bool bl;
                            if (appointment.Blacklist == "Da")
                                bl = true;
                            else
                                bl = false;

                            newPatient = new Patient
                            {
                                FirstName = appointment.FirstName,
                                SecondName = appointment.SecondName,
                                PhonNumber = appointment.PhonNumber,
                                Mail = appointment.Mail,
                                Blacklist = bl
                            };

                            SheetPatient FisaPacientCurent = new SheetPatient();
                            _context.Add(FisaPacientCurent);
                            await _context.SaveChangesAsync();

                            int lastID = _context.SheetPatient.Count();
                            newPatient.SheetPatientId = lastID;
                            _context.Add(newPatient);
                            await _context.SaveChangesAsync();
                        }

                        var newAppointment = new Appointment
                        {
                            Id = appointment.Id,
                            PatientId = newPatient.Id,
                            MedicId = appointment.MedicId,
                            RoomId = appointment.RoomId,
                            AppointmentDate = appointment.AppointmentDate,
                            AppointmentHour = appointment.AppointmentHour,
                            Procedure = appointment.Procedure,
                            Made = appointment.Made,
                            ResponsibleForAppointment = appointment.ResponsibleForAppointment,
                            AppointmentCreationDate = appointment.AppointmentCreationDate,
                            Comments = appointment.Comments,
                            Hidden = appointment.Hidden
                        };

                        _context.Add(newAppointment);

                        await _context.SaveChangesAsync();
                        return "Ok";
                    }
                }
            }
            catch(Exception ex)
            {
                return "Error." + ex.Message;
            }
        }

        #endregion
        /**************************************************************************************/
        #region Get

        public async Task<AppointmentViewModel> GetAppointmentViewModelByIdAsync(Appointment Appointment)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AppointmentViewModel> GetAppointmentByIdAsync(int Id)
        {
            try
            {
                var queri = await (
                    from patient in _context.Patient
                    join appointment in _context.Appointment on patient.Id equals appointment.PatientId
                    join room in _context.Room on appointment.RoomId equals room.Id
                    join medic in _context.Medic on appointment.MedicId equals medic.Id
                    select _agendaViewsFactory.PrepereAppointmentViewModel(appointment, patient, medic, room)
                    ).ToListAsync();


                return queri.FirstOrDefault(a => a.Id == Id);
            }
            catch
            {
                return new AppointmentViewModel();
            }
        }

        public async Task<MVCAgendaViewsManager> GetAppointmentsAsync(string SearchByName, string SearchByPhoneNumber, string SearchByEmail, string SearchByAppointmentHour, string SearchByAppointmentDate, int SearchByRoom, int SearchByMedic, int Id, bool Daily, bool Hidden)
        {
            try
            {
                var queriAppointmentsList = await (
                    from patient in _context.Patient
                    join appointment in _context.Appointment

                        .Where(h => Hidden == true ? h.Hidden == true : h.Hidden == false)
                        .Where(d => Daily == true ? d.AppointmentDate.Contains(DayTime) : true)
                        .Where(p => Id != 0 ? p.PatientId == Id : true)
                        .Where(a => SearchByMedic != 0 ? a.MedicId == SearchByMedic : true)
                        .Where(a => SearchByRoom != 0 ? a.RoomId == SearchByRoom : true)
                        .Where(a => !string.IsNullOrEmpty(SearchByAppointmentHour) ? a.AppointmentHour.Contains(SearchByAppointmentHour) : true)
                        .Where(a => !string.IsNullOrEmpty(SearchByAppointmentDate) ? a.AppointmentDate.Contains(SearchByAppointmentDate) : true)
                            on patient.Id equals appointment.PatientId

                    join room in _context.Room on appointment.RoomId equals room.Id
                    join medic in _context.Medic on appointment.MedicId equals medic.Id
                    orderby (appointment.AppointmentHour)
                    select _agendaViewsFactory.PrepereAppointmentViewModel(appointment, patient, medic, room)
                    ).ToListAsync();

                queriAppointmentsList = queriAppointmentsList
                    .Where(p => !string.IsNullOrEmpty(SearchByName) ? p.FirstName.Contains(SearchByName) : true)
                    .Where(p => !string.IsNullOrEmpty(SearchByPhoneNumber) ? p.PhonNumber.Contains(SearchByPhoneNumber) : true)
                    .Where(p => !string.IsNullOrEmpty(SearchByEmail) ? p.Mail.Contains(SearchByEmail) : true).ToList();

                return new MVCAgendaViewsManager()
                {
                    AppointmentsList = queriAppointmentsList
                };
            }
            catch
            {
                return new MVCAgendaViewsManager();
            }
        }

        #endregion
        /**************************************************************************************/
        #region Edit

        public async Task<string> EditAppointmentAsync(AppointmentViewModel appointment)
        {
            try
            {
                var appointmentToUpdate = new Appointment()
                {
                    Id = appointment.Id,
                    PatientId = appointment.PatientId,
                    MedicId = appointment.MedicId,
                    RoomId = appointment.RoomId,
                    AppointmentDate = appointment.AppointmentDate,
                    AppointmentHour = appointment.AppointmentHour,
                    Procedure = appointment.Procedure,
                    Made = appointment.Made,
                    Hidden = appointment.Hidden,
                    AppointmentCreationDate = appointment.AppointmentCreationDate,
                    ResponsibleForAppointment = appointment.ResponsibleForAppointment,
                    Comments = appointment.Comments
                };
                _context.Appointment.Update(appointmentToUpdate);
                await _context.SaveChangesAsync();
                return "Ok";
            }
            catch(Exception ex)
            {
                return "Error. " + ex.Message; 
            }
        }

        #endregion
        /**************************************************************************************/
        #region Hide

        public async Task<string> HideAppointmentAsync(int id)
        {
            try
            {
                var appointment = await _context.Appointment.FindAsync(id);
                appointment.Hidden = true;
                _context.Appointment.Update(appointment);
                await _context.SaveChangesAsync();
                return "";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
        /**************************************************************************************/
        #region Delete

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            try
            {
                var appointment = await _context.Appointment.FindAsync(id);
                _context.Appointment.Remove(appointment);
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
        #region Utils

        private string searchAppointment(int MedicId, int RoomId, string AppointmentDate, string AppointmentHour)
        {
            string foundAppointment = "Clear";

            //Search a medic, date, h
            var appointmentsM = _context.Appointment
                        .Where(p => p.MedicId == MedicId)
                        .Where(p => p.AppointmentDate == AppointmentDate)
                        .Where(p => p.AppointmentHour == AppointmentHour)
                        .Where(p => p.Hidden == false)
                        .ToList();
            if (appointmentsM.Count >= 1)
            {
                var medic = _context.Medic.FirstOrDefaultAsync(m => m.Id == MedicId);
                foundAppointment = $"{medic.Result.MedicName} este ocupat/a la data {AppointmentDate}, ora {AppointmentHour}.";
                return foundAppointment;
            }

            //Search a room, date, h
            var appointmentsR = _context.Appointment
                        .Where(p => p.RoomId == RoomId)
                        .Where(p => p.AppointmentDate == AppointmentDate)
                        .Where(p => p.AppointmentHour == AppointmentHour)
                        .Where(p => p.Hidden == false)
                        .ToList();

            if (appointmentsR.Count >= 1)
            {
                var room = _context.Room.FirstOrDefaultAsync(m => m.Id == RoomId);
                foundAppointment = $"{room.Result.RoomName} este ocupata la data {AppointmentDate}, ora {AppointmentHour}.";
                return foundAppointment;
            }

            return foundAppointment;
        }

        #endregion
    }
}

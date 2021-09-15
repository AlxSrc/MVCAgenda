using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.Logging;

namespace MVCAgenda.Data.DataBaseManager
{
    public class AgendaContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public AgendaContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientSheet> PatientsSheet { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Medic> Medics { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
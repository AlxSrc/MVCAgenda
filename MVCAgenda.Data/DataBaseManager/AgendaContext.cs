using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;

namespace MVCAgenda.Data
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

        public DbSet<PatientDto> PatientDto { get; set; }
        public DbSet<SheetPatientDto> SheetPatientDto { get; set; }
        public DbSet<AppointmentDto> AppointmentDto { get; set; }
        public DbSet<ConsultationDto> ConsultationDto { get; set; }
        public DbSet<RoomDto> RoomDto { get; set; }
        public DbSet<MedicDto> MedicDto { get; set; }
    }
}
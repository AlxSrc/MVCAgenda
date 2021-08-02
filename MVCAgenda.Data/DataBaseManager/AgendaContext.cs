using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;

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

        public DbSet<Patient> Patient { get; set; }
        public DbSet<SheetPatient> SheetPatient { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Consultation> Consultation { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Medic> Medic { get; set; }
    }
}
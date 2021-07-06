using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Domain;

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

        public DbSet<Pacient> Pacient { get; set; }
        public DbSet<FisaPacient> FisaPacient { get; set; }
        public DbSet<Consultatie> Consultatie { get; set; }
        public DbSet<Programare> Programare { get; set; }
        public DbSet<Camera> Camera { get; set; }
        public DbSet<Medic> Medic { get; set; }
    }
}
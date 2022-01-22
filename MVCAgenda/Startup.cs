using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Factories.Appointments;
using MVCAgenda.Factories.Consultations;
using MVCAgenda.Factories.Medics;
using MVCAgenda.Factories.Patients;
using MVCAgenda.Factories.PatientsSheet;
using MVCAgenda.Factories.Rooms;
using MVCAgenda.Factories.Scheduler;
using MVCAgenda.Managers.Appointments;
using MVCAgenda.Managers.Consultations;
using MVCAgenda.Managers.Medics;
using MVCAgenda.Managers.Patients;
using MVCAgenda.Managers.PatientsSheets;
using MVCAgenda.Managers.Rooms;
using MVCAgenda.Managers.Scheduler;
using MVCAgenda.Service.Appointments;
using MVCAgenda.Service.Consultations;
using MVCAgenda.Service.Logins;
using MVCAgenda.Service.Medics;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.Rooms;
using MVCAgenda.Service.PatientsSheet;
using MVCAgenda.Managers.ManageAccount;
using MVCAgenda.Managers.Roles;
using Microsoft.AspNetCore.Authorization;
using MVCAgenda.Managers.Logging;
using MVCAgenda.Factories.Logging;
using MVCAgenda.Factories.Home;
using MVCAgenda.Core;
using MVCAgenda.Framework;
using Nop.Services.Helpers;
using MVCAgenda.Service.Helpers;
using MVCAgenda.Core.DateTimeHelper;

namespace MVCAgenda
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            //services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddDbContext<AgendaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AgendaContext")));


            string connectionString = Configuration.GetConnectionString("AgendaContext");
            services.AddDbContext<AgendaContext>(c => c.UseSqlServer(connectionString));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AgendaContext>();

            services.AddHttpContextAccessor();

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<AgendaContext>();

            //services
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IPatientSheetService, PatientSheetService>();
            services.AddScoped<IConsultationService, ConsultationService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IMedicService, MedicService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IWorkContext, WebWorkContext>();
            services.AddScoped<IDateTimeHelper, DateTimeHelper>();
            services.AddScoped<DateTimeSettings>();

            //Factories
            services.AddScoped<IHomeFactory, HomeFactory>();
            services.AddScoped<IPatientsFactory, PatientsFactory>();
            services.AddScoped<IPatientsSheetsFactory, PatientsSheetsFactory>();
            services.AddScoped<IConsultationsFactory, ConsultationsFactory>();
            services.AddScoped<IAppointmentsFactory, AppointmentsFactory>();
            services.AddScoped<IMedicsFactory, MedicsFactory>();
            services.AddScoped<IRoomsFactory, RoomsFactory>();
            services.AddScoped<ISchedulerFactory, SchedulerFactory>();
            services.AddScoped<ILoggingFactory, LoggingFactory>();

            //Managers
            services.AddScoped<IPatientsManager, PatientsManager>();
            services.AddScoped<IPatientsSheetsManager, PatientsSheetsManager>();
            services.AddScoped<IConsultationsManager, ConsultationsManager>();
            services.AddScoped<IAppointmentsManager, AppointmentsManager>();
            services.AddScoped<IMedicsManager, MedicsManager>();
            services.AddScoped<IRoomsManager, RoomsManager>();
            services.AddScoped<ISchedulerManager, SchedulerManager>();
            services.AddScoped<IManageAccountManager, ManageAccountManager>();
            services.AddScoped<IRolesManager, RolesManager>();
            services.AddScoped<ILoggingManager, LoggingManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
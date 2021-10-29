using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MVCAgenda.ApiHost.Converters;
using MVCAgenda.ApiHost.Factories.Appointments;
using MVCAgenda.ApiHost.Factories.Items;
using MVCAgenda.ApiHost.Factories.Patients;
using MVCAgenda.ApiHost.JSON.Serializers;
using MVCAgenda.ApiHost.Managers;
using MVCAgenda.ApiHost.Maps;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Appointments;
using MVCAgenda.Service.Consultations;
using MVCAgenda.Service.Logins;
using MVCAgenda.Service.Medics;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.PatientsSheet;
using MVCAgenda.Service.Rooms;

namespace MVCAgenda.ApiHost
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
            services.AddControllers();

            services.AddDbContext<AgendaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AgendaContext")));

            string connectionString = Configuration.GetConnectionString("AgendaContext");
            services.AddDbContext<AgendaContext>(c => c.UseSqlServer(connectionString));

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "MVCAgenda.ApiHost", Version = "v1" }); });

            //services
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IPatientSheetService, PatientSheetService>();
            services.AddScoped<IConsultationService, ConsultationService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IMedicService, MedicService>();
            services.AddScoped<IAppointmentService, AppointmentService>();

            services.AddScoped<IJsonFieldsSerializer, JsonFieldsSerializer>();
            services.AddScoped<IJsonPropertyMapper, JsonPropertyMapper>();

            //Factories
            services.AddScoped<IItemsFactory, ItemsFactory>();
            services.AddScoped<IPatientsFactory, PatientsFactory>();
            services.AddScoped<IAppointmentsFactory, AppointmentsFactory>();
            services.AddScoped<IObjectConverter, ObjectConverter>(); 
            services.AddScoped<IApiTypeConverter, ApiTypeConverter>();

            services.AddScoped<IItemsManager, ItemsManager>(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MVCAgenda.ApiHost v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
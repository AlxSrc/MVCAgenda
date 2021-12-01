using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MVCAgenda.ApiHost.Converters;
using MVCAgenda.ApiHost.Factories.Appointments;
using MVCAgenda.ApiHost.Factories.Consultations;
using MVCAgenda.ApiHost.Factories.Items;
using MVCAgenda.ApiHost.Factories.Medics;
using MVCAgenda.ApiHost.Factories.Patients;
using MVCAgenda.ApiHost.Factories.PatientSheets;
using MVCAgenda.ApiHost.Factories.Rooms;
using MVCAgenda.ApiHost.JSON.Serializers;
using MVCAgenda.ApiHost.Managers;
using MVCAgenda.ApiHost.Maps;
using MVCAgenda.Core;
using MVCAgenda.Core.Helpers;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Framework;
using MVCAgenda.Service.Appointments;
using MVCAgenda.Service.Consultations;
using MVCAgenda.Service.Logins;
using MVCAgenda.Service.Medics;
using MVCAgenda.Service.Patients;
using MVCAgenda.Service.PatientsSheet;
using MVCAgenda.Service.Rooms;
using System.Text;
using System.Threading.Tasks;

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
            var key = Encoding.ASCII.GetBytes(Constants.ApyKey);

            services.AddControllers();
            services.AddControllers(opt =>
            {
                var policy = new AuthorizationPolicyBuilder("Bearer").RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddDbContext<AgendaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AgendaContext")));

            string connectionString = Configuration.GetConnectionString("AgendaContext");
            services.AddDbContext<AgendaContext>(c => c.UseSqlServer(connectionString));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AgendaContext>();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "MVCAgenda.ApiHost", Version = "v1" }); });
            services.AddHttpContextAccessor();

            services.AddAuthentication(x =>
            {
               x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
               x.Events = new JwtBearerEvents
               {
                   OnTokenValidated = context =>
                   {
                       var userMachine = context.HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
                       var user = userMachine.GetUserAsync(context.HttpContext.User);

                       if (user == null)
                           context.Fail("UnAuthorized");

                       return Task.CompletedTask;
                   }
               };
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };

            });


            //services
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IPatientSheetService, PatientSheetService>();
            services.AddScoped<IConsultationService, ConsultationService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IMedicService, MedicService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IWorkContext, WebWorkContext>();

            services.AddScoped<IJsonFieldsSerializer, JsonFieldsSerializer>();
            services.AddScoped<IJsonPropertyMapper, JsonPropertyMapper>();

            //Factories
            services.AddScoped<IItemsFactory, ItemsFactory>();
            services.AddScoped<IPatientsFactory, PatientsFactory>();
            services.AddScoped<IPatientSheetFactory, PatientSheetFactory>();
            services.AddScoped<IConsultationFactory, ConsultationFactory>();
            services.AddScoped<IAppointmentsFactory, AppointmentsFactory>();
            services.AddScoped<IMedicsFactory, MedicsFactory>();
            services.AddScoped<IRoomsFactory, RoomsFactory>();
            services.AddScoped<IObjectConverter, ObjectConverter>();
            services.AddScoped<IApiTypeConverter, ApiTypeConverter>();

            services.AddScoped<IItemsManager, ItemsManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

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
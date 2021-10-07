using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MVCAgenda.ApiHost.Factories.Patients;
using MVCAgenda.ApiHost.JSON.Serializers;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Logins;
using MVCAgenda.Service.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
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

            services.AddControllers();

            services.AddDbContext<AgendaContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AgendaContext")));

            string connectionString = Configuration.GetConnectionString("AgendaContext");
            services.AddDbContext<AgendaContext>(c => c.UseSqlServer(connectionString));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MVCAgenda.ApiHost", Version = "v1" });
            });

            //services
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IJsonFieldsSerializer, JsonFieldsSerializer>(); 

            //Factories
            services.AddScoped<IPatientsFactory, PatientsFactory>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

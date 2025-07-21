using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PatientAppointmentBackend.Data.Contexts;
using PatientAppointmentBackend.Service.Services;
using PatientAppointmentBackend.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PatientAppointmentBackend.Service
{
    public class Startup
    {
        private IWebHostEnvironment HostingEnvironment { get; }
        public IConfiguration Configuration { get; }

        private ILoggerFactory _loggerFactory;
        private readonly ILogger<Startup> _logger;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            HostingEnvironment = env;
            Configuration = configuration;
            _loggerFactory = new LoggerFactory();
            _logger = _loggerFactory.CreateLogger<Startup>();
        }

        /// <summary>
        /// Configure Services 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("fr-FR")
                };
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

            });


            services.AddControllers().AddJsonOptions(configure => 
            {
                configure.JsonSerializerOptions.WriteIndented = true;
                configure.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                configure.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            }).AddMvcLocalization()
            .AddDataAnnotationsLocalization();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PatientAppointmentService", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "SwaggerAnnotation.xml"));
                c.OperationFilter<AcceptLanguageHeaderAttribute>();
            });
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("PatientAppointmentDb")));
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IAppointmentStateService, AppointmentStateService>();

            services.AddHostedService<HostedServiceManager>();
            
        }

        /// <summary>
        /// Configure the Runtime
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PatientAppointmentService");
            });

            app.UseRouting();
            app.UseRequestLocalization();

            app.UseEndpoints(endpoints =>
            {
            endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "");

                endpoints.MapGet("/", context =>
                {
                    return Task.Run(() => context.Response.Redirect("/swagger/index.html"));
                });
            });
            
            
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PatientAppointmentBackend.Data.Contexts;
using PatientAppointmentBackend.Data.Entities;
using PatientAppointmentBackend.Service;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var startup = new Startup(builder.Environment, builder.Configuration);
startup.ConfigureServices(builder.Services);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("Program.cs");
    try
    {
        var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        ctx.Database.Migrate();
        logger.LogInformation("Database Migrated at Startup");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, $"Exception migrating database: {ex.Message}");
    }
}

//Seed some data
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("Program.cs");
    try
    {
        var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var clinician = new Clinician()
        {
            Name = "Dr Smith"
        };

        //var exists = ctx.Clinicians.Find(c => c.Name == "Dr John");
        var t = ctx.Clinicians.Where(x => x.Name == "Dr Smith").ToList();
        if(!t.Any())
        {
            ctx.Clinicians.Add(clinician);
            ctx.SaveChanges();
        }

        var tall = ctx.Clinicians.ToList();
        
        logger.LogInformation("Data seeded");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, $"Exception migrating database: {ex.Message}");
    }
}

startup.Configure(app, app.Environment);

app.Run();
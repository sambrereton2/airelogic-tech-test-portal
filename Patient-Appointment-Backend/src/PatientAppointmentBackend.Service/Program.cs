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

startup.Configure(app, app.Environment);

app.Run();
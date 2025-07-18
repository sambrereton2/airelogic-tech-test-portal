using PatientAppointmentBackend.Data.Contexts;
using PatientAppointmentBackend.Data.Entities;
using PatientAppointmentBackend.Service.Services.Interfaces;

namespace PatientAppointmentBackend.Service.Services
{
    /// <summary>
    /// Check the state of booked meetings
    /// </summary>
    public class AppointmentStateService : IAppointmentStateService
    {
        private readonly ILogger<AppointmentStateService> _logger;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="context"></param>
        public AppointmentStateService(ILogger<AppointmentStateService> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        /// <summary>
        /// Check the database for missed meetings
        /// </summary>
        /// <returns></returns>
        public void CheckForMissedMeetings()
        {
            // Get a list of meetings that are Booked and have expired            
            var missedMeetings = _context.Appointments.Where(x => x.EndTime <  DateTime.UtcNow && x.Status == AppointmentStatus.Booked).ToList();
            if (missedMeetings.Any())
            {
                _logger.LogInformation("Updating {MISSED_MEETINGS}", missedMeetings.Count);
                foreach (var item in missedMeetings)
                {
                    item.Status = AppointmentStatus.Missed;                    
                }
                _context.Appointments.UpdateRange(missedMeetings);
                _context.SaveChanges();
            }
            else
            {
                _logger.LogInformation("No missed meetings to update.");
            }
        }
    }
}


using PatientAppointmentBackend.Service.Services.Interfaces;

namespace PatientAppointmentBackend.Service.Services
{
    /// <summary>
    /// Background service to check for missed appointments and update their status
    /// </summary>
    public class HostedServiceManager : BackgroundService
    {
        private readonly ILogger<HostedServiceManager> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly System.Timers.Timer _refreshTimer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceProvider"></param>
        public HostedServiceManager(ILogger<HostedServiceManager> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            _refreshTimer = new System.Timers.Timer();
            _refreshTimer.Interval = 60000;
            _refreshTimer.AutoReset = true;
            _refreshTimer.Elapsed += RefreshState;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await StartAsync(stoppingToken);
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
                // Can't use stoppingToken because it has now being signalled!
                await StopAsync(CancellationToken.None);
                Environment.Exit(0);
            }
            catch (OperationCanceledException)
            {
                // When the stopping token is canceled, for example, a call made from services.msc,
                // we shouldn't exit with a non-zero exit code. In other words, this is expected...
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}", ex.Message);

                // Terminates this process and returns an exit code to the operating system.
                // This is required to avoid the 'BackgroundServiceExceptionBehavior', which
                // performs one of two scenarios:
                // 1. When set to "Ignore": will do nothing at all, errors cause zombie services.
                // 2. When set to "StopHost": will cleanly stop the host, and log errors.
                //
                // In order for the Windows Service Management system to leverage configured
                // recovery options, we need to terminate the process with a non-zero exit code.
                Environment.Exit(1);
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("HostedServiceManager Started");
            await StartUpdates(5000, 15);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delay">ms delay to first update</param>
        /// <param name="interval">minutes between each subsequent update</param>
        /// <returns></returns>
        private async Task StartUpdates(int delay, int interval)
        {
            _logger.LogInformation("Starting Checks");
            await Task.Delay(delay);

            _logger.LogInformation("Performing first check");
            CheckMeetingState();

            _logger.LogInformation($"Enabling Periodic Configuration Checks at every {interval} minutes.");
            _refreshTimer.Interval = interval * 60000;
            _refreshTimer.Start();
        }

        private void RefreshState(object? sender, System.Timers.ElapsedEventArgs e)
        {
            _logger.LogInformation("HostedServiceManager - Update Configuration");
            CheckMeetingState();
        }

        private void CheckMeetingState()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IAppointmentStateService>();
                service.CheckForMissedMeetings();
            }

        }
    }
}

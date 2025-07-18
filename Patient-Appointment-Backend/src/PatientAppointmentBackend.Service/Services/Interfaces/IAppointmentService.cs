using PatientAppointmentBackend.Service.Models;

namespace PatientAppointmentBackend.Service.Services.Interfaces
{
    public interface IAppointmentService
    {
        /// <summary>
        /// Create a new appointment
        /// </summary>
        /// <param name="newAppointment"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AppointmentId> CreateAppointment(NewAppointment newAppointment, CancellationToken cancellationToken);

        /// <summary>
        /// Attend a booked appointment
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Attend(AppointmentId appointmentId, CancellationToken cancellationToken);

        /// <summary>
        /// Cancel a booked appointment
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Cancel(AppointmentId appointmentId, CancellationToken cancellationToken);
    }
}

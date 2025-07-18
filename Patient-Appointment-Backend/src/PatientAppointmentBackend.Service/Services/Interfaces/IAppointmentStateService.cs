namespace PatientAppointmentBackend.Service.Services.Interfaces
{
    public interface IAppointmentStateService
    {
        /// <summary>
        /// Check the database for missed meetings
        /// </summary>
        /// <returns></returns>
        void CheckForMissedMeetings();
    }
}

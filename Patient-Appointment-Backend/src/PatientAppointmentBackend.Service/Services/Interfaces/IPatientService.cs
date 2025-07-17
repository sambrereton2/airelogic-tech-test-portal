using PatientAppointmentBackend.Service.Models;

namespace PatientAppointmentBackend.Service.Services.Interfaces
{
    /// <summary>
    /// Implement's patient CRUD methods
    /// </summary>
    public interface IPatientService
    {
        /// <summary>
        /// Add a new patient record
        /// </summary>
        /// <param name="newPatient"></param>
        /// <param name="withUpdate">True if requesting an update</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <remarks>If the patient exists, and withUpdate is false, this will throw</remarks>
        Task AddPatientAsync(NewPatient newPatient, bool withUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Find a patient record by their NhsNumber
        /// </summary>
        /// <param name="nhsNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PatientModel?> GetPatientAsync(string nhsNumber, CancellationToken cancellationToken);

    }
}

using Microsoft.EntityFrameworkCore;
using PatientAppointmentBackend.Data.Contexts;
using PatientAppointmentBackend.Data.Entities;
using PatientAppointmentBackend.Service.Models;
using PatientAppointmentBackend.Service.Models.Transformers;
using PatientAppointmentBackend.Service.Services.Interfaces;
using PatientAppointmentBackend.Shared.Validators;

namespace PatientAppointmentBackend.Service.Services
{
    /// <summary>
    /// Implements functionality to create/update/delete patients
    /// </summary>
    public class PatientService : IPatientService
    {
        private readonly ILogger<PatientService> _logger;   
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="context"></param>
        public PatientService(ILogger<PatientService> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Add or update a Patient record
        /// </summary>
        /// <param name="newPatient"></param>
        /// <param name="withUpdate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task AddPatientAsync(NewPatient newPatient, bool withUpdate, CancellationToken cancellationToken = default)
        {
            var exists = _context.Patients.Any(x => x.NhsNumber == newPatient.NhsNumber);
            //var f = _context.Patients.First(x => x.NhsNumber == newPatient.NhsNumber);

            if(exists && !withUpdate)
            {
                throw new Exception("Patient already exists");
            }

            PostCodeValidator.Validate(newPatient.Postcode, out string postcode);

            if(exists)
            {
                // Update Patient
                var currentPatient = _context.Patients.First(x => x.NhsNumber == newPatient.NhsNumber);
                if (currentPatient != null)
                {
                    currentPatient.PostCode = postcode;
                    currentPatient.Name = newPatient.Name;
                    currentPatient.DateOfBirth = newPatient.DateOfBirth;
                    _context.Patients.Update(currentPatient);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Could not find Patient to update");
                }
            }
            else
            {
                var patient = new Patient()
                {
                    NhsNumber = newPatient.NhsNumber,
                    Name = newPatient.Name,
                    DateOfBirth = newPatient.DateOfBirth,
                    PostCode = postcode
                };
                await _context.Patients.AddAsync(patient);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Find a patient record by their NhsNumber
        /// </summary>
        /// <param name="nhsNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PatientModel?> GetPatientAsync(string nhsNumber, CancellationToken cancellationToken)
        {
            var patient = _context.Patients.FirstOrDefault(x => x.NhsNumber == nhsNumber);
            if (patient != null)
            {
                return PatientModelTransformer.From(patient);
            }
            return null;
        }
    }
}

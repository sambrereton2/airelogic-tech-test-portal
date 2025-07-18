using PatientAppointmentBackend.Data.Contexts;
using PatientAppointmentBackend.Data.Entities;
using PatientAppointmentBackend.Service.Models;
using PatientAppointmentBackend.Service.Services.Interfaces;
using PatientAppointmentBackend.Shared.Validators;

namespace PatientAppointmentBackend.Service.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ILogger<AppointmentService> _logger;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="context"></param>
        public AppointmentService(ILogger<AppointmentService> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }



        /// <summary>
        /// Create a new appointment
        /// </summary>
        /// <param name="newAppointment"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AppointmentId> CreateAppointment(NewAppointment newAppointment, CancellationToken cancellationToken)
        {
            var patient = _context.Patients.FirstOrDefault(x => x.NhsNumber == newAppointment.Patient);
            if (patient == null) 
            {
                throw new Exception("Patient does not exist");
            }

            var clinician = FindOrCreateClinician(newAppointment.Clinician);
            var department = FindOrCreateDepartment(newAppointment.Department, newAppointment.Postcode);

            DurationValidator.Validate(newAppointment.Duration, out TimeSpan duration);

            var id = new AppointmentId()
            {
                Id = Guid.NewGuid(),
            };

            var appointment = new Appointment()
            {
                Patient = patient,
                Clinician = clinician,
                Department = department,
                Time = newAppointment.Time,
                Status = AppointmentStatus.Booked,
                EndTime = newAppointment.Time.Add(duration),
                Id = id.Id
            };
            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return id;
        }

        /// <summary>
        /// Attend a booked appointment
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Attend(AppointmentId appointmentId, CancellationToken cancellationToken)
        {
            UpdateAppointmentState(appointmentId.Id, AppointmentStatus.Attended);
        }

        /// <summary>
        /// Cancel a booked appointment
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Cancel(AppointmentId appointmentId, CancellationToken cancellationToken)
        {
            UpdateAppointmentState(appointmentId.Id, AppointmentStatus.Cancelled);
        }

        private void UpdateAppointmentState(Guid id, AppointmentStatus status)
        {
            var appointment = _context.Appointments.FirstOrDefault(x => x.Id == id);
            if (appointment == null)
            {
                throw new Exception("Appointment does not exist");
            }
            if (status == AppointmentStatus.Attended)
            {
                DateTime startWindow = appointment.Time.AddMinutes(-30);
                if(!(DateTime.UtcNow >  startWindow && DateTime.UtcNow < appointment.EndTime))
                {
                    throw new Exception("You cannot mark an appointment as being attended earlier than 30 mins of it's shceduled start time");
                }
            }
            if (appointment.EndTime < DateTime.UtcNow)
            {
                throw new Exception("This appointment has already finished");
            }
            appointment.Status = status;
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
        }

        private Clinician FindOrCreateClinician(string name)
        {
            var normalisedName = name.ToUpper();
            var clinician = _context.Clinicians.FirstOrDefault(x => x.Name == normalisedName);
            if(clinician != null)
            {
                return clinician;
            }

            clinician = new Clinician() { Name = normalisedName };
            _context.Clinicians.Add(clinician);
            // SaveChanges will be done when the appointment is saved
            return clinician;
        }

        private Department FindOrCreateDepartment(string name, string postCode)
        {
            PostCodeValidator.Validate(postCode, out string validatedPostcode);
            var normalisedName = name.ToUpper();

            var dept = _context.Departments.FirstOrDefault(x => x.Name == normalisedName);
            if(dept != null)
            {
                return dept;
            }
            
            dept = new Department() { Name = normalisedName, Postcode = validatedPostcode };
            _context.Departments.Add(dept);
            // SaveChanges will be done when the appointment is saved
            return dept;
        }
    }
}

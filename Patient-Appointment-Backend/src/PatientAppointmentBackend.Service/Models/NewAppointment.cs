using PatientAppointmentBackend.Service.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace PatientAppointmentBackend.Service.Models
{
    public class NewAppointment
    {
        /// <summary>
        /// The Patient's Nhs Number
        /// </summary>
        /// <example>1373645350</example>
        [Required]
        [StringLength(10, ErrorMessage = ResourceKeys.NhsNumberMustBe10, MinimumLength = 10)]
        [NhsNumberValidation(ErrorMessage = ResourceKeys.NhsNumberInvalid)]
        public string Patient { get; set; }

        /// <summary>
        /// The start time of the appointment
        /// </summary>
        /// <example>2025-08-10T11:30:00+00:00</example>
        [Required]
        [AppointmentTimeValidation(ErrorMessage = "Appointment time must be in the Future")]
        public DateTime Time { get; set; }

        /// <summary>
        /// The duration (minutes) in the form 15m
        /// </summary>
        /// <example>15m</example>
        [Required]
        [DurationValidation(ErrorMessage = ResourceKeys.DurationValidation)]
        public string Duration { get; set; }

        /// <summary>
        /// Name of Clinician
        /// </summary>
        /// <example>Dr Smith</example>
        [Required]
        public string Clinician { get; set; }

        /// <summary>
        /// The Department
        /// </summary>
        /// <example>Oncology</example>
        [Required]
        public string Department { get; set; }

        /// <summary>
        /// The Department's Postcode
        /// </summary>
        /// <example>LS20 8AX</example>
        [PostCodeValidation(ErrorMessage = "Postcode Not Valid")]
        public string Postcode { get; set; }
    }
}

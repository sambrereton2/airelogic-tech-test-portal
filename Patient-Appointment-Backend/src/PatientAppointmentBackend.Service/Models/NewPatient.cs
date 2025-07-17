using PatientAppointmentBackend.Service.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace PatientAppointmentBackend.Service.Models
{
    /// <summary>
    /// Post body to create a new patient
    /// </summary>
    public class NewPatient
    {
        /// <summary>
        /// The Patient's unique Nhs Number
        /// </summary>
        /// <example>1373645350</example>
        [Required]
        [StringLength(10, ErrorMessage = "Nhs Number must be 10 characters long", MinimumLength = 10)]
        [NhsNumberValidation(ErrorMessage = "Not a valid Nhs Number")]
        public string? NhsNumber { get; set; }

        /// <summary>
        /// The Patient's name
        /// </summary>
        /// <example>Mr John Smith</example>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// The Patient's Date of Birth
        /// </summary>
        /// <example>2075-05-22T00:00:00+00:00</example>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// The Patient's Postcode
        /// </summary>
        /// <example>LS20 8AX</example>
        [PostCodeValidation(ErrorMessage = "Not a valid Uk Post Code")]
        public string? PostCode { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using PatientAppointmentBackend.Shared.Validators;

namespace PatientAppointmentBackend.Service.Models.Validators
{
    /// <summary>
    /// Validates a UK PostCode field
    /// </summary>
    public class PostCodeValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PostCodeValidationAttribute()
        {

        }

        /// <summary>
        /// Check the postcode is valid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object? value)
        {
            return PostCodeValidator.Validate(value, out _);
        }
    }
}

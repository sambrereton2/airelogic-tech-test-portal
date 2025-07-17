using System.ComponentModel.DataAnnotations;

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

        public override bool IsValid(object? value)
        {
            return false;
        }
    }
}

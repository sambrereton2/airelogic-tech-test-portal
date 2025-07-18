using PatientAppointmentBackend.Shared.Validators;
using System.ComponentModel.DataAnnotations;

namespace PatientAppointmentBackend.Service.Models.Validators
{
    /// <summary>
    /// Validate the duration value
    /// </summary>
    public class DurationValidationAttribute : ValidationAttribute
    {
        public DurationValidationAttribute()
        {

        }

        public override bool IsValid(object? value)
        {            
            return DurationValidator.Validate(value, out TimeSpan _);
        }
    }
}

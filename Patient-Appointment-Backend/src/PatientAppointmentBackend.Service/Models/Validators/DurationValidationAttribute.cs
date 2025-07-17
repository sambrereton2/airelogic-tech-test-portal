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
            if (value is null)
            {
                return false;
            }
            string? strValue = Convert.ToString(value);
            if (string.IsNullOrEmpty(strValue))
            {
                return false;
            }
            return NhsNumberValidator.Validate(strValue);
        }
    }
}

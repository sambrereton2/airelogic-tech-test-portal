using PatientAppointmentBackend.Shared.Validators;
using System.ComponentModel.DataAnnotations;

namespace PatientAppointmentBackend.Service.Models.Validators
{
    public class NhsNumberValidationAttribute : ValidationAttribute
    {
        public NhsNumberValidationAttribute()
        {

        }

        public override bool IsValid(object? value)
        {
            if(value is null)
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

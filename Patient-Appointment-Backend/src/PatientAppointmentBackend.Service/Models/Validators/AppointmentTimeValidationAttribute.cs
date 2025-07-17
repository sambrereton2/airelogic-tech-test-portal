using System.ComponentModel.DataAnnotations;

namespace PatientAppointmentBackend.Service.Models.Validators
{
    /// <summary>
    /// Check that the DateTime value of an appointment is in the future
    /// </summary>
    public class AppointmentTimeValidationAttribute : ValidationAttribute
    {
        public AppointmentTimeValidationAttribute()
        {

        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return false;
            }
            DateTime? dt = Convert.ToDateTime(value);

            if (!dt.HasValue)
            {
                return false;
            }

            if (dt.Value < DateTime.UtcNow)
            {
                return false;
            }

            return true;
        }
    }
}

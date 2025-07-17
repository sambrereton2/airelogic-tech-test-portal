using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientAppointmentBackend.Shared.Validators
{
    public static class DurationValidator
    {
        public static bool Validate(object? value, out TimeSpan duration)
        {
            duration = TimeSpan.Zero;
            if (value is null)
            {
                return false;
            }
            string? strValue = Convert.ToString(value);
            if (string.IsNullOrEmpty(strValue))
            {
                return false;
            }

            try
            {
                if (strValue.ToLower()[strValue.Length - 1] == 'm')
                {
                    var numeric = strValue.Substring(0, strValue.Length - 1);
                    var minutes = Convert.ToInt32(numeric);
                    duration = TimeSpan.FromMinutes(minutes);
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }

            return false;
        }
    }
}

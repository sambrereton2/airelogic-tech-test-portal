using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientAppointmentBackend.Shared.Validators
{
    public static class NhsNumberValidator
    {
        public static bool Validate(object? value)
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

            if(strValue.Length != 10)
            {
                return false;
            }

            if(!strValue.All(Char.IsDigit))
            {
                return false;   
            }

            // Calculate checksum
            int weightedResults = 0;
            for(int i= 0; i< 9; i++)
            {
                weightedResults += ((int)char.GetNumericValue(strValue[i]) * (10 - i));
            }
            var remainder = weightedResults % 11;
            var checkDigit = 11 - remainder;
            if(checkDigit == 11)
            {
                checkDigit = 0;
            }

            if (checkDigit == 10)
            {
                return false;
            }

            if(checkDigit == (int)char.GetNumericValue(strValue[9]))
            {
                return true;
            }

            return false;
        }
    }
}

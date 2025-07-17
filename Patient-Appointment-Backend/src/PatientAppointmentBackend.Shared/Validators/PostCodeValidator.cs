using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientAppointmentBackend.Shared.Validators
{
    public static class PostCodeValidator
    {
        public static bool Validate(object? value, out string postCode)
        {
            postCode = string.Empty;
            if(value is null)
            {
                return false;
            }

            var strValue = value as string;
            if (strValue is null)
            {
                return false;
            }

            var noWhiteSpace = strValue.Replace(" ", "");
            // Check for invalid characters
            if (!noWhiteSpace.All(Char.IsLetterOrDigit))
            {
                return false;
            }
            if (noWhiteSpace.Length < 5)
            {
                // eg, L1 3SZ
                return false;
            }
            if (noWhiteSpace.Length > 7)
            {
                // eg, SW1A 2AA
                return false;
            }
            string incode = noWhiteSpace.Substring(noWhiteSpace.Length - 3);
            //Check incode format
            bool incodeValid = false;
            if (char.IsNumber(incode[0]) && char.IsLetter(incode[1]) && char.IsLetter(incode[2]))
            {
                incodeValid = true;
            }
            if(!incodeValid)
            {
                return false;
            }
            string outcode = noWhiteSpace[..^incode.Length];

            if (!char.IsLetter(outcode[0]))
            {
                return false;
            }
            // TODO - evaluate for different lengths of outcode (2 to 4 digits).

            postCode = $"{outcode} {incode}".ToUpper();


            return true;
        }
    }
}

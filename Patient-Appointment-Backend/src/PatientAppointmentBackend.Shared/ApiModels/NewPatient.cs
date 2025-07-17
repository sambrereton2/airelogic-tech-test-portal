using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientAppointmentBackend.Shared.ApiModels
{
    public class NewPatient2
    {
        /// <summary>
        /// The Patient's unique Nhs Number
        /// </summary>
        /// <example>1373645350</example>
        
        public string NhsNumber { get; set; }

        /// <summary>
        /// The Patient's name
        /// </summary>
        /// <example>Mr John Smith</example>
        public string Name { get; set; }

        /// <summary>
        /// The Patient's Date of Birth
        /// </summary>
        /// <example>2075-05-22T00:00:00+00:00</example>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// The Patient's Postcode
        /// </summary>
        /// <example>LS20 8AX</example>
        public string PostCode { get; set; }
    }
}

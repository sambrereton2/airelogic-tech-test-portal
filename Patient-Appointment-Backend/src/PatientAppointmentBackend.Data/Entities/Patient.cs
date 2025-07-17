using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientAppointmentBackend.Data.Entities
{
    /// <summary>
    /// Unique registered patients
    /// </summary>
    [Table(nameof(Patient))]
    public class Patient
    {
        public Guid Id { get; set; }
        public string NhsNumber { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PostCode { get; set; }
    }
}

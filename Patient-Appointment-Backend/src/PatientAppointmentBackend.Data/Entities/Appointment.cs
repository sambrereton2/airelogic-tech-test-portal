using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientAppointmentBackend.Data.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }

        public Patient Patient { get; set; }

        public Clinician Clinician { get; set; }

        public Department Department { get; set; }

        public AppointmentStatus Status { get; set; }

        public DateTime Time {  get; set; }

        public DateTime EndTime { get; set; }

    }
}

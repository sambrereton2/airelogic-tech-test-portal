using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientAppointmentBackend.Data.Entities
{
    public enum AppointmentStatus
    {
        Booked,
        Attended,
        Missed,
        Cancelled
    }
}

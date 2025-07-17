namespace PatientAppointmentBackend.Service.Models
{
    public class PatientModel
    {
        public string NhsNumber { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PostCode { get; set; }
    }
}

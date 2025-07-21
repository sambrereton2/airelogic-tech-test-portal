namespace PatientAppointmentBackend.Service.Models
{
    public static class ResourceKeys
    {
        public const string NhsNumberInvalid = "Not a valid Nhs Number";
        public const string PatientCreated = "Patient Created";
        public const string PatientUpdated = "Patient Updated";
        public const string PostcodeInvalid = "Not a valid Uk Post Code";
        public const string NhsNumberMustBe10 = "Nhs Number must be 10 characters long";
        public const string DurationValidation = "Appointment Duration must be specified in the format 15m";
        public const string AppointmentTimeMustBeInFuture = "Appointment time must be in the Future";
    }
}

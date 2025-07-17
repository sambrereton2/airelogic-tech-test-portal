using PatientAppointmentBackend.Data.Entities;

namespace PatientAppointmentBackend.Service.Models.Transformers
{
    /// <summary>
    /// Conversions To/From PatientModel
    /// </summary>
    public static class PatientModelTransformer
    {
        /// <summary>
        /// Transform Patient to PatientModel
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public static PatientModel From(Patient patient)
        {
            return new PatientModel()
            {
                NhsNumber = patient.NhsNumber,
                Name = patient.Name,
                DateOfBirth = patient.DateOfBirth,
                Postcode = patient.PostCode,
            };
        }
    }
}

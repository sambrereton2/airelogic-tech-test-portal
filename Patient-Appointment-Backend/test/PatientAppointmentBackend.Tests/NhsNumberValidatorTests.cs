using PatientAppointmentBackend.Shared.Validators;

namespace PatientAppointmentBackend.Tests
{
    public class NhsNumberValidatorTests
    {
        [Fact]
        public void Test_Valid_Number()
        {
            // Arrange
            var nhsNumber = "1373645350";

            // Act
            var result = NhsNumberValidator.Validate(nhsNumber);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Test_InValid_Number()
        {
            // Arrange
            var nhsNumber = "1373645357";

            // Act
            var result = NhsNumberValidator.Validate(nhsNumber);

            // Assert
            Assert.False(result);
        }
    }
}
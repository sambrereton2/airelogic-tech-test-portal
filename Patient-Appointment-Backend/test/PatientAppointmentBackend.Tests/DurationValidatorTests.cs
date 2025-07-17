using PatientAppointmentBackend.Shared.Validators;

namespace PatientAppointmentBackend.Tests
{
    public class DurationValidatorTests
    {
        [Fact]
        public void Test_Valid_Duration()
        {
            // Arrange
            var input = "15m";

            // Act
            var result = DurationValidator.Validate(input, out TimeSpan duration);

            // Assert
            Assert.True(result);
            Assert.Equal(15, duration.TotalMinutes);
        }

        [Fact]
        public void Test_Valid_InDuration()
        {
            // Arrange
            var input = "15s";

            // Act
            var result = DurationValidator.Validate(input, out TimeSpan duration);

            // Assert
            Assert.False(result);
            Assert.Equal(0, duration.TotalMinutes);
        }

        [Fact]
        public void Test_Valid_InDuration2()
        {
            // Arrange
            var input = "15";

            // Act
            var result = DurationValidator.Validate(input, out TimeSpan duration);

            // Assert
            Assert.False(result);
            Assert.Equal(0, duration.TotalMinutes);
        }
    }
}

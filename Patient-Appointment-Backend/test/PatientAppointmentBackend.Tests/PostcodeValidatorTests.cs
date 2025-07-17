using PatientAppointmentBackend.Shared.Validators;

namespace PatientAppointmentBackend.Tests
{
    public class PostcodeValidatorTests
    {
        [Fact]
        public void Test_Valid_Postcode()
        {
            // Arrange
            var input = "YO88QN";

            // Act
            var result = PostCodeValidator.Validate(input, out string postcode);

            // Assert
            Assert.True(result);
            Assert.Equal("YO8 8QN", postcode);
        }

        [Fact]
        public void Test_Valid_Postcode2()
        {
            // Arrange
            var input = "    yo8    8qn  ";

            // Act
            var result = PostCodeValidator.Validate(input, out string postcode);

            // Assert
            Assert.True(result);
            Assert.Equal("YO8 8QN", postcode);
        }

        [Fact]
        public void Test_Valid_Postcode3()
        {
            // Arrange
            var input = "sw1a 2aa";

            // Act
            var result = PostCodeValidator.Validate(input, out string postcode);

            // Assert
            Assert.True(result);
            Assert.Equal("SW1A 2AA", postcode);
        }

        [Fact]
        public void Test_InValid_Postcode1()
        {
            // Arrange
            var input = "yo&8qn";

            // Act
            var result = PostCodeValidator.Validate(input, out string postcode);

            // Assert
            Assert.False(result);
            Assert.Equal("", postcode);
        }
    }
}

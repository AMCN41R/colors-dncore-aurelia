using ColorsTest.Core.Repositories.People;
using Xunit;

namespace ColorsTest.UnitTests.Core.People
{
    public class PersonTests
    {
        [Theory]
        [InlineData(null, null, "")]
        [InlineData("", "", "")]
        [InlineData(" ", " ", "")]
        [InlineData("", " ", "")]
        [InlineData("", null, "")]
        [InlineData(" ", "", "")]
        [InlineData(" ", null, "")]
        [InlineData(null, "", "")]
        [InlineData(null, " ", "")]
        [InlineData("", "Smith", "Smith")]
        [InlineData("John", "", "John")]
        [InlineData("John", "Smith", "John Smith")]
        [InlineData(" John ", " Smith ", "John Smith")]
        public void Person_FullName_ReturnsExpectedString(string first, string last, string expected)
        {
            // Arrange
            var person = new Person
            {
                Id = 1,
                FirstName = first,
                LastName = last
            };

            // Act
            var fullName = person.FullName;

            // Assert
            Assert.Equal(expected, fullName);
        }

        [Theory]
        [InlineData(null, null, false)]
        [InlineData("", "", false)]
        [InlineData(" ", " ", false)]
        [InlineData("", " ", false)]
        [InlineData("", null, false)]
        [InlineData(" ", "", false)]
        [InlineData(" ", null, false)]
        [InlineData(null, "", false)]
        [InlineData(null, " ", false)]
        [InlineData("", "Smith", false)]
        [InlineData("John", "", false)]
        [InlineData("John", "Smith", false)]
        [InlineData(" John ", " Smith ", false)]
        [InlineData("Bo", "Bob", true)]
        [InlineData("Joan", "Naoj", true)]
        public void Person_IsPalindrome_ReturnsExpectedBoolean(string first, string last, bool expected)
        {
            // Arrange
            var person = new Person
            {
                Id = 1,
                FirstName = first,
                LastName = last
            };

            // Act
            var isPalindrome = person.IsPalindrome;

            // Assert
            Assert.Equal(expected, isPalindrome);
        }
    }
}
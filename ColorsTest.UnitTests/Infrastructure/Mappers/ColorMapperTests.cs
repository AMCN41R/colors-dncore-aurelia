using System;
using ColorsTest.Core.Colors;
using ColorsTest.Infrastructure.Mappers;
using ColorsTest.Infrastructure.Repositories.Colors;
using Xunit;

namespace ColorsTest.UnitTests.Infrastructure.Mappers
{
    public class ColorMapperTests
    {
        #region ToEntity

        [Fact]
        public void ToEntity_NullColor_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => (null as Color).ToEntity()
            );
        }

        [Fact]
        public void ToEntity_ValidColor_ReturnsExpectedColorEntity()
        {
            // Arrange
            var sut = new Color
            {
                Id = 123,
                Name = "red",
                IsEnabled = true
            };

            // Act
            var result = sut.ToEntity();

            // Assert
            Assert.Equal(123, result.ColourId);
            Assert.Equal("red", result.Name);
            Assert.True(result.IsEnabled);
        }

        #endregion

        #region ToModel

        [Fact]
        public void ToModel_NullEntity_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => (null as ColorEntity).ToModel()
            );
        }

        [Fact]
        public void ToModel_ValidColor_ReturnsExpectedEntity()
        {
            // Arrange
            var sut = new ColorEntity
            {
                ColourId = 123,
                Name = "red",
                IsEnabled = true
            };

            // Act
            var result = sut.ToModel();

            // Assert
            Assert.Equal(123, result.Id);
            Assert.Equal("red", result.Name);
            Assert.True(result.IsEnabled);
        }

        #endregion
    }
}
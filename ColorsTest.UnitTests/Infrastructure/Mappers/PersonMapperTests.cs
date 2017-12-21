using System;
using System.Collections.Generic;
using ColorsTest.Core.Colors;
using ColorsTest.Core.Repositories.People;
using ColorsTest.Infrastructure.Mappers;
using ColorsTest.Infrastructure.Repositories.People;
using Xunit;

namespace ColorsTest.UnitTests.Infrastructure.Mappers
{
    public class PersonMapperTests
    {
        #region FromEntity

        [Fact]
        public void FromEntity_NullEntityAndNoColors_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => (null as PersonEntity).FromEntity()
            );
        }

        [Fact]
        public void FromEntity_NullEntityWithColors_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => (null as PersonEntity).FromEntity(new List<Color>())
            );
        }

        #endregion

        #region

        [Fact]
        public void ToEntity_NullModelAndNoColors_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => (null as Person).ToEntity()
            );
        }

        [Fact]
        public void ToEntity_ValidModel_ReturnsExpectedEntity()
        {
            // Arrange
            var model = new Person
            {
                Id = 123,
                FirstName = "Marty",
                LastName = "McFly",
                IsAuthorised = true,
                IsValid = true,
                IsEnabled = true,
                Colors = new List<Color>
                {
                    new Color
                    {
                        Id = 1,
                        Name = "Red",
                        IsEnabled = true
                    }
                }
            };

            var expected = new PersonEntity
            {
                PersonId = 123,
                FirstName = "Marty",
                LastName = "McFly",
                IsAuthorised = true,
                IsValid = true,
                IsEnabled = true
            };

            // Act
            var result = model.ToEntity();

            // Assert
            //Assert.Equal(expected, result, COMPARER);
        }

        #endregion
    }
}
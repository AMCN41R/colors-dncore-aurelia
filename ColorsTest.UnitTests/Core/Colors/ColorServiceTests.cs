using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColorsTest.Core.Colors;
using NSubstitute;
using Xunit;

namespace ColorsTest.UnitTests.Core.Colors
{
    public class ColorServiceTests
    {
        #region AddColor

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task AddColor_NullEmptyOrWhitespaceString_ThrowsArgumentNullException(string value)
        {
            // Arrange
            var sut = this.GetSubjectUnderTest();

            // Act / Assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await sut.AddColor(value)
            );

        }

        [Theory]
        [InlineData("green")]
        [InlineData(" green")]
        [InlineData("green ")]
        [InlineData(" green ")]
        public async Task AddColor_ValidName_AddsColor(string value)
        {
            // Arrange
            var sut = this.GetSubjectUnderTest();

            // Act
            await sut.AddColor(value);

            // Assert
            await this
                .ColorRepository
                .Received()
                .AddOrUpdate(Arg.Is<Color>(x =>
                    x.IsEnabled
                    && x.Name == "green"
                    && x.Id == default(int))
                );
        }

        #endregion

        #region CanAdd

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task CanAdd_NullEmptyOrWhitespaceString_ThrowsArgumentNullException(string value)
        {
            // Arrange
            var sut = this.GetSubjectUnderTest();

            // Act / Assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await sut.CanAdd(value)
            );
        }

        [Fact]
        public async Task CanAdd_NoColors_ReturnsTrue()
        {
            // Arrange
            this.ColorRepository
                .Get()
                .Returns(new List<Color>());

            var sut = this.GetSubjectUnderTest();

            // Act
            var result = await sut.CanAdd("red");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CanAdd_ColorDoesNotExist_ReturnsTrue()
        {
            // Arrange
            this.ColorRepository
                .Get()
                .Returns(new List<Color>
                {
                    new Color{Name = "green"},
                    new Color{Name = "blue"}
                });

            var sut = this.GetSubjectUnderTest();

            // Act
            var result = await sut.CanAdd("red");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CanAdd_ColorExists_ReturnsFalse()
        {
            // Arrange
            this.ColorRepository
                .Get()
                .Returns(new List<Color>
                {
                    new Color{Name = "green"},
                    new Color{Name = "blue"},
                    new Color{Name = "red"}
                });

            var sut = this.GetSubjectUnderTest();

            // Act
            var result = await sut.CanAdd("red");

            // Assert
            Assert.False(result);
        }

        #endregion

        #region CanDelete

        [Fact]
        public async Task CanDelete_NoFavourites_ReturnsTrue()
        {
            // Arrange
            this.ColorRepository
                .GetFavourites()
                .Returns(new List<Color>());

            var sut = this.GetSubjectUnderTest();

            // Act
            var result = await sut.CanDelete(3);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CanDelete_IdThatIsNotFavourite_ReturnsTrue()
        {
            // Arrange
            this.ColorRepository
                .GetFavourites()
                .Returns(new List<Color>
                {
                    new Color{Id = 1},
                    new Color{Id = 2}
                });

            var sut = this.GetSubjectUnderTest();

            // Act
            var result = await sut.CanDelete(3);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CanDelete_IdThatIsFavourite_ReturnsFalse()
        {
            // Arrange
            this.ColorRepository
                .GetFavourites()
                .Returns(new List<Color>
                {
                    new Color{Id = 1},
                    new Color{Id = 2},
                    new Color{Id = 3}
                });

            var sut = this.GetSubjectUnderTest();

            // Act
            var result = await sut.CanDelete(3);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region DeleteColor

        [Fact]
        public async Task DeleteColor_ValidId_DeletesColor()
        {
            // Arrange
            var sut = this.GetSubjectUnderTest();

            // Act
            await sut.DeleteColor(123);

            // Assert
            await this
                .ColorRepository
                .Received()
                .Delete(123);
        }

        #endregion

        #region GetColor

        [Fact]
        public async Task GetColor_ValidId_GetsColor()
        {
            // Arrange
            this.ColorRepository
            .Get(Arg.Any<int>())
            .Returns(new Color
            {
                Id = 123,
                Name = "red",
                IsEnabled = true
            });

            var sut = this.GetSubjectUnderTest();

            // Act
            var result = await sut.GetColor(123);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(123, result.Id);
            Assert.Equal("red", result.Name);
            Assert.True(result.IsEnabled);

            await this
                .ColorRepository
                .Received()
                .Get(123);
        }

        #endregion

        #region GetColors

        [Fact]
        public async Task GetColors_MethodCall_GetsAllColors()
        {
            // Arrange
            this.ColorRepository
                .Get()
                .Returns(new List<Color>
                {
                    new Color{Id = 123, Name = "red", IsEnabled = true},
                    new Color{Id = 456, Name = "green", IsEnabled = false}
                });
            var sut = this.GetSubjectUnderTest();

            // Act
            var result = (await sut.GetColors()).ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            Assert.Equal(123, result[0].Id);
            Assert.Equal("red", result[0].Name);
            Assert.True(result[0].IsEnabled);

            Assert.Equal(456, result[1].Id);
            Assert.Equal("green", result[1].Name);
            Assert.False(result[1].IsEnabled);

            await this
                .ColorRepository
                .Received()
                .Get();
        }

        #endregion


        #region Setup

        private IColorRepository ColorRepository { get; } = Substitute.For<IColorRepository>();

        private ColorService GetSubjectUnderTest()
        {
            return new ColorService(this.ColorRepository);
        }

        #endregion
    }
}
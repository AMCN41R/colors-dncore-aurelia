using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ColorsTest.Controllers;
using ColorsTest.Core.Colors;
using ColorsTest.UnitTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace ColorsTest.UnitTests.Controllers
{
    public class ColorsControllerTests
    {
        [Fact]
        public void Controller_RouteAttribute_IsPresentWithCorrectRoute()
        {
            Custom.Assert.ControllerHasRouteAttribute<ColorsController>("api/colors");
        }

        #region GetColors

        [Fact]
        public void GetColors_RestVerbAttribute_IsPresentWithCorrectTemplate()
        {
            Custom.Assert.ControllerMethodHasVerb<ColorsController, HttpGetAttribute>(nameof(ColorsController.GetColors));
        }

        [Fact]
        public async Task GetColors_MethodCall_ReturnsOkWithExpectedColors()
        {
            // Arange
            this.ColorService
                .GetColors()
                .Returns(new List<Color>
                {
                    new Color{ Id = 1, Name = "red", IsEnabled = true },
                    new Color{ Id = 2, Name = "green", IsEnabled = true }
                });

            var sut = this.GetSubjectUnderTest();

            // Act
            var response = await sut.GetColors();

            // Assert
            Assert.IsType<OkObjectResult>(response);

            var result = response.Convert<List<Color>>();
            Assert.Equal(2, result.Count);

            Assert.Equal(1, result[0].Id);
            Assert.Equal("red", result[0].Name);
            Assert.True(result[0].IsEnabled);

            Assert.Equal(2, result[1].Id);
            Assert.Equal("green", result[1].Name);
            Assert.True(result[1].IsEnabled);
        }

        #endregion

        #region AddColor

        [Fact]
        public void AddColor_RestVerbAttribute_IsPresentWithCorrectTemplate()
        {
            Custom.Assert.ControllerMethodHasVerb<ColorsController, HttpPostAttribute>(nameof(ColorsController.AddColor), "{name}");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task AddColor_NullEmptyOrWhitespaceName_ReturnsBadRequest(string value)
        {

        }

        [Fact]
        public async Task AddColor_ColorThatCannotBeAdded_ReturnsPreconditionFailed()
        {

        }

        [Fact]
        public async Task AddColor_ValidName_AddsColorAndReturnsOk()
        {

        }

        #endregion

        #region CanDeleteColor

        [Fact]
        public void CanDeleteColor_RestVerbAttribute_IsPresentWithCorrectTemplate()
        {
            Custom.Assert.ControllerMethodHasVerb<ColorsController, HttpGetAttribute>(nameof(ColorsController.CanDeleteColor), "{id}/can-delete");
        }

        [Fact]
        public async Task CanDeleteColor_ValidId_ReturnsOkWithExpectedResult()
        {
            // Arange
            var id = 123;

            this.ColorService
                .CanDelete(Arg.Any<int>())
                .Returns(true);

            var sut = this.GetSubjectUnderTest();

            // Act
            var response = await sut.CanDeleteColor(id);

            // Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.True(response.Convert<bool>());
            await this.ColorService.Received().CanDelete(id);
        }

        #endregion

        #region DeleteColor

        [Fact]
        public void DeleteColor_RestVerbAttribute_IsPresentWithCorrectTemplate()
        {
            Custom.Assert.ControllerMethodHasVerb<ColorsController, HttpDeleteAttribute>(nameof(ColorsController.DeleteColor), "{id}");
        }

        [Fact]
        public async Task DeleteColor_IdThatCannotBeDeleted_ReturnsPreconditionFailed()
        {

        }

        [Fact]
        public async Task DeleteColor_ValidId_DeletesColorAndReturnsOk()
        {

        }

        #endregion

        #region Setup

        private IColorService ColorService { get; } = Substitute.For<IColorService>();

        private ColorsController GetSubjectUnderTest()
        {
            return new ColorsController(this.ColorService);
        }

        #endregion
    }
}
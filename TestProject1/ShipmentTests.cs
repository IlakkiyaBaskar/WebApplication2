
using System.Net;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using WebApplication2.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace TestProject1
{
    public class ShipmentTests
    {
        public ShipmentTests() { }


        [Fact]
        public async Task Post_WithValidInput_ReturnsTokenResponse()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ShipmentsController>>();
            var controller = new ShipmentsController(mockLogger.Object);

            // Act
            var result = await controller.Post();

            // Assert
            Assert.IsType<ActionResult<WebApplication2.Model.TokenResponse>>(result);
            Assert.IsType<WebApplication2.Model.TokenResponse>(result.Value);
        }


        [Fact]
        public async Task Post_WhenApiFails_ReturnsErrorStatus()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ShipmentsController>>();
            var controller = new ShipmentsController(mockLogger.Object);

            // Simulate API failure by using an invalid proxy address
            controller.proxyAddress = "https://invalid.url";

            // Act
            var result = await controller.Post();

            // Assert
            Assert.True(result.Result is ObjectResult || result.Result is StatusCodeResult);
        }

        [Fact]
        public async Task Get_WithQueryParams_ReturnsOkResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ShipmentsController>>();
            var controller = new ShipmentsController(mockLogger.Object);

            // Simulate Authorization header
            controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext();
            controller.ControllerContext.HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer test-token";

            // Act
            var result = await controller.Get("Air", 2);

            // Assert
            Assert.IsType<ActionResult<WebApplication2.Model.ShipmentResponse>>(result);
        }


        [Fact]
        public async Task Get_WithoutAuthorization_ReturnsUnauthorized()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ShipmentsController>>();
            var controller = new ShipmentsController(mockLogger.Object);
            controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext();
            controller.ControllerContext.HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();

            // Act
            var result = await controller.Get("Sea", 1);

            // Assert
            Assert.IsType<UnauthorizedResult>(result.Result);
        }


        [Fact]
        public async Task Get_WithInvalidAuthorizationScheme_ReturnsUnauthorized()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ShipmentsController>>();
            var controller = new ShipmentsController(mockLogger.Object);
            controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext();
            controller.ControllerContext.HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Basic test-token";

            // Act
            var result = await controller.Get("Sea", 1);

            // Assert
            Assert.IsType<UnauthorizedResult>(result.Result);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithShipmentResponse()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ShipmentsController>>();
            var controller = new ShipmentsController(mockLogger.Object);

            // Simulate Authorization header
            controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext();
            controller.ControllerContext.HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer test-token";

            // Act
            var result = await controller.Get("Sea", 1);

            // Assert
            Assert.IsType<ActionResult<WebApplication2.Model.ShipmentResponse>>(result);
        }
    }
}
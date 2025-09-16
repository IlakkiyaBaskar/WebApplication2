
using System.Net;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using WebApplication2.Controllers;

namespace TestProject1
{
    public class ShipmentTests
    {
        public ShipmentTests() { 
        
        
        }
        [Fact]
        public async void Test1()
        {
            var mockLogger = new Mock<ILogger<ShipmentsController>>();
            var mockHttp = new Mock<HttpMessageHandler>();

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp
        .Protected()
        .Setup<Task<HttpResponseMessage>>(
            "GetAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        )
        .ReturnsAsync(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("Mocked response content")
        });

            // Inject the handler or client into your application code
            var client = new HttpClient(mockHttp.Object);
            var expectedValue = new string[] { "value1", "value2" };
          var response = client.GetAsync(client.BaseAddress).GetAwaiter().GetResult();

            var controller = new ShipmentsController(mockLogger.Object);

            // Act
            var result = await controller.Get("",1);

            // Assert
            //var okResult = Assert.IsType<OkObjectResult>(result);
            //var actualProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(result.Result.ToString(), expectedValue.ToString());
            //Assert.Equal(expectedProduct.Name, actualProduct.Name);



        }
    }
}
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace CloudCustomers.UnitTest.Helpers; 

internal static class MockHttpMessageHandler<T> {
    internal static Mock<HttpMessageHandler> SetupBasicGetResourceList (List<T> expectedResponse) {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) {
            Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
        };
        return CreateMockMessageHandler(mockResponse);
    }
    
    internal static Mock<HttpMessageHandler> SetupReturn404 () {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.NotFound) {
            Content = new StringContent(JsonConvert.SerializeObject(""))
        };
        return CreateMockMessageHandler(mockResponse);
    }

    private static Mock<HttpMessageHandler> CreateMockMessageHandler(HttpResponseMessage mockResponse) {
        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var MockMessageHandler = new Mock<HttpMessageHandler>();
        MockMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        return MockMessageHandler;
    }
}
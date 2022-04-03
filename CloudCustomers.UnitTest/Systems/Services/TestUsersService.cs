using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTest.Fixtures;
using CloudCustomers.UnitTest.Helpers;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Xunit;

namespace CloudCustomers.UnitTest.Systems.Services; 

public class TestUsersService {
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesHttpRequest() {
        // Arrange
        var users = UsersFixture.GetUsers();
        var mockHttpHandler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(users);
        var subjectUnderTest = new UserService(new HttpClient(mockHttpHandler.Object));

        // Act
        var response = await subjectUnderTest.GetAllUsers();

        // Assert
        mockHttpHandler.Protected().Verify(
            "SendAsync", 
            Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(request => request.Method == HttpMethod.Get),
            ItExpr.IsAny<CancellationToken>()
            );
    }
        
}
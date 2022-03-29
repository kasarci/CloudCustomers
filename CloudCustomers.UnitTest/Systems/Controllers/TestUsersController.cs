using System.Threading.Tasks;
using CloudCustomers.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CloudCustomers.UnitTest.Systems.Controllers;

public class TestUsersController {
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200() {
        // Arrange
        var usersController = new UsersController();
        
        //Act
        var result = (ObjectResult) await usersController.Get();
        
        //Assert
        result.StatusCode.Should().Be(200);
    }
}
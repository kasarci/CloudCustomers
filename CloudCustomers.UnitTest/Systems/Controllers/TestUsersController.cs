using System.Collections.Generic;
using System.Threading.Tasks;
using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CloudCustomers.UnitTest.Systems.Controllers;

public class TestUsersController {
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200() {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        
        var usersController = new UsersController(mockUserService.Object);
        
        //Act
        var result = (ObjectResult) await usersController.Get();
        
        //Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokeUserServiceExactlyOnce() {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        
        var usersController = new UsersController(mockUserService.Object);
        
        // Act
        var result = (ObjectResult) await usersController.Get();
        
        // Assert
        mockUserService.Verify(service => service.GetAllUsers(), Times.Once);
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfUsers() {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var userController = new UsersController(mockUserService.Object);
        
        //Act
        var result = (ObjectResult) await userController.Get();
        
        //Assert
        result.Value.Should().BeOfType<List<User>>("Get method of the user controller should return the list of users.");
    }
}
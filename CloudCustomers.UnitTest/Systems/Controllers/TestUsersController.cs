using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTest.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CloudCustomers.UnitTest.Systems.Controllers;

public class TestUsersController {
    
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200() {
        // Arrange
        var usersController = CreateMockUsersController(UsersFixture.GetUsers());
        
        //Act
        var result = (ObjectResult) await usersController.Get();
        
        //Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokeUserServiceExactlyOnce() {
        // Arrange
        var usersController = CreateMockUsersController(out var mockUserService);
        
        // Act
        var result = (ObjectResult) await usersController.Get();
        
        // Assert
        mockUserService.Verify(service => service.GetAllUsers(), Times.Once);
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfUsers() {
        // Arrange
        var userController = CreateMockUsersController(UsersFixture.GetUsers());

        //Act
        var result = (ObjectResult) await userController.Get();
        
        //Assert
        result.Value.Should().BeOfType<List<User>>("Get method of the user controller should return the list of users.");
    }

    [Fact]
    public async Task Get_OnNotFound_Returns404() {
        // Arrange
        var userController = CreateMockUsersController();
        
        //Act
        var result = (ObjectResult) await userController.Get();
        
        //Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        result.StatusCode.Should().Be(404);

    }

    private static UsersController CreateMockUsersController() {
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var userController = new UsersController(mockUserService.Object);
        return userController;
        
    }
    private static UsersController CreateMockUsersController(List<User> users) {
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(users);

        var userController = new UsersController(mockUserService.Object);
        return userController;
        
    }
    
    private static UsersController CreateMockUsersController(out Mock<IUserService> outUserService) {
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetUsers());

        var userController = new UsersController(mockUserService.Object);
        outUserService = mockUserService;
        return userController;
    }
}
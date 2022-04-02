using System.Collections.Generic;
using CloudCustomers.API.Models;

namespace CloudCustomers.UnitTest.Fixtures; 

public static class UsersFixture {
    public static List<User> GetUsers() => new() {
        new User() {
            Id = 1,
            Name = "Test User 1",
            Email = "test@test.com",
            Address = new Address() {
                Street = "Test",
                City = "Test",
                ZipCode = "12345"
            }
        },
        new User() {
            Id = 2,
            Name = "Test User 2",
            Email = "test@test.com",
            Address = new Address() {
                Street = "Test",
                City = "Test",
                ZipCode = "12345"
            }
        }
    };
}
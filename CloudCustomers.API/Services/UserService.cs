using System.Net;
using System.Text.Json.Serialization;
using CloudCustomers.API.Models;

namespace CloudCustomers.API.Services; 

public class UserService : IUserService {

    private readonly HttpClient _httpClient;
    public UserService(HttpClient httpClient) {
        _httpClient = httpClient;
    }

    public async Task<List<User>> GetAllUsers() {
        var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
        if (response.StatusCode == HttpStatusCode.NotFound) {
            return new List<User>();    
        }
        var users = await response.Content.ReadFromJsonAsync<List<User>>();
        return users;
    }
}

public interface IUserService {
    public Task<List<User>> GetAllUsers();
}
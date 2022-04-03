using CloudCustomers.API.Models;

namespace CloudCustomers.API.Services; 

public class UserService : IUserService {

    private readonly HttpClient _httpClient;
    public UserService(HttpClient httpClient) {
        _httpClient = httpClient;
    }

    public async Task<List<User>> GetAllUsers() {
        await _httpClient.GetAsync("https://test.com");
        return new List<User>();
    }
}

public interface IUserService {
    public Task<List<User>> GetAllUsers();
}
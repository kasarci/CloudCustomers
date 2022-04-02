using CloudCustomers.API.Models;

namespace CloudCustomers.API.Services; 

public class UserService : IUserService{
    public UserService() { }
    public Task<List<User>> GetAllUsers() {
        throw new NotImplementedException();
    }
}

public interface IUserService {
    public Task<List<User>> GetAllUsers();
}
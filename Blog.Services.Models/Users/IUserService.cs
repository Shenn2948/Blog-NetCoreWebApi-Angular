using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Services.Users
{
    public interface IUserService
    {
        Task<User> AddUserAsync(UpdateUserRequest userIn);

        Task<User> UpdateUserAsync(string id, UpdateUserRequest userIn);

        Task DeleteUserAsync(string id);

        Task<User> GetUserAsync(string id);

        Task<IEnumerable<User>> GetUsersAsync();
    }
}
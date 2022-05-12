using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Core.Models;

namespace TodoList.Core.ervices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> CreateAsync(User user);
        Task UpdateAsync(User updatedUser, User user);
        Task DeleteAsync(User user);
    }
}
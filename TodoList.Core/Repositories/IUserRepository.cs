using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Core.Models;

namespace TodoList.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllWithTodosAsync();
        Task<User> GetWithTodosByIdAsync(int id);
        Task<IEnumerable<User>> GetAllWithTodosByUserIdAsync(int userId);
    }
}
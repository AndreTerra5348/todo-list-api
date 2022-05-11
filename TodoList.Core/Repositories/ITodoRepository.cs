using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Core.Models;

namespace TodoList.Core.Repositories
{
    public interface ITodoRepository : IRepository<Todo>
    {
        Task<IEnumerable<Todo>> GetAllWithUserAsync();
        Task<Todo> GetWithUserByIdAsync(int id);
        Task<IEnumerable<Todo>> GetAllWithUserByUserIdAsync(int userId);
    }
}
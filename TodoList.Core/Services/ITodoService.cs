using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Core.Models;

namespace TodoList.Core.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetAllWithUserAsync();
        Task<IEnumerable<Todo>> GetByUserIdAsync(int id);
        Task<Todo> GetByIdAsync(int id);
        Task<Todo> CreateAsync(Todo todo);
        Task UpdateAsync(Todo todo, Todo updatedTodo);
        Task DeleteAsync(Todo todo);
    }
}
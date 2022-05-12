using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Core.Models;

namespace TodoList.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetAllWithUserAsync();
        Task<IEnumerable<Todo>> GetByUserIdAsync(int id);
        Task<Todo> GetByIdAsync(int id);
        Task<Todo> CreateAsync(Todo todo);
        Task UpdateAsync(Todo updatedTodo, Todo todo);
        Task DeleteAsync(Todo todo);
    }
}
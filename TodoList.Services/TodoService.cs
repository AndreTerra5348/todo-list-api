using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Core;
using TodoList.Core.Models;
using TodoList.Core.Services;

namespace TodoList.Services
{
    public class TodoService : ITodoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Todo> CreateAsync(Todo todo)
        {
            await _unitOfWork.Todos.CreateAsync(todo);
            await _unitOfWork.CommitAsync();
            return todo;
        }

        public async Task DeleteAsync(Todo todo)
        {
            _unitOfWork.Todos.Delete(todo);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Todo>> GetAllWithUserAsync()
        {
            return await _unitOfWork.Todos.GetAllWithUserAsync();
        }

        public async Task<Todo> GetByIdAsync(int id)
        {
            return await _unitOfWork.Todos.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Todo>> GetByUserIdAsync(int id)
        {
            return await _unitOfWork.Todos.GetAllWithUserByUserIdAsync(id);
        }

        public async Task UpdateAsync(Todo updatedTodo, Todo todo)
        {
            updatedTodo.IsDone = todo.IsDone;
            updatedTodo.Title = todo.Title;
            updatedTodo.UserId = todo.UserId;

            await _unitOfWork.CommitAsync();
        }
    }
}
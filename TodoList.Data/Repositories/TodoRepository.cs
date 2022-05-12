using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoList.Core.Models;
using TodoList.Core.Repositories;

namespace TodoList.Data.Repositories
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        private new TodoListDbContext Context
        {
            get { return base.Context as TodoListDbContext; }
        }

        public TodoRepository(TodoListDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Todo>> GetAllWithUserAsync()
        {
            return await Context.Todos.Include(x => x.User).ToListAsync();
        }

        public async Task<IEnumerable<Todo>> GetAllWithUserByUserIdAsync(int userId)
        {
            return await Context.Todos.Include(m => m.User)
                .Where(m => m.UserId == userId)
                .ToListAsync();
        }

        public async Task<Todo> GetWithUserByIdAsync(int id)
        {
            return await Context.Todos.Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
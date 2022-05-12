using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoList.Core.Models;
using TodoList.Core.Repositories;

namespace TodoList.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private new TodoListDbContext Context
        {
            get { return base.Context as TodoListDbContext; }
        }

        public UserRepository(TodoListDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetAllWithTodosAsync()
        {
            return await Context.Users.Include(u => u.Todos).ToListAsync();
        }

        public async Task<User> GetWithTodosByIdAsync(int id)
        {
            return await Context.Users.Include(u => u.Todos).FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
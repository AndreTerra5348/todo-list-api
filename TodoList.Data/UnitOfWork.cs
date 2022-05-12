using System.Threading.Tasks;
using TodoList.Core;
using TodoList.Core.Repositories;
using TodoList.Data.Repositories;

namespace TodoList.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoListDbContext _context;
        private TodoRepository _todoRepository;
        private UserRepository _userRepository;


        public IUserRepository Users => _userRepository ?? (_userRepository = new UserRepository(_context));

        public ITodoRepository Todos => _todoRepository ?? (_todoRepository = new TodoRepository(_context));

        public UnitOfWork(TodoListDbContext context)
        {
            _context = context;
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
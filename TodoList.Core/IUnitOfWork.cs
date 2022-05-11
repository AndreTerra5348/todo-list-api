using System;
using System.Threading.Tasks;
using TodoList.Core.Repositories;

namespace TodoList.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        ITodoRepository Todos { get; }
        Task<int> CommitAsync();
    }
}
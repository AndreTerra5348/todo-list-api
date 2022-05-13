using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoList.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        void Delete(T entity);
    }
}
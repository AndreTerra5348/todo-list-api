using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Core;
using TodoList.Core.ervices;
using TodoList.Core.Models;

namespace TodoList.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _unitOfWork.Users.CreateAsync(user);
            await _unitOfWork.CommitAsync();
            return user;
        }

        public async Task DeleteAsync(User user)
        {
            _unitOfWork.Users.Delete(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _unitOfWork.Users.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _unitOfWork.Users.GetByIdAsync(id);
        }

        public async Task UpdateAsync(User updatedUser, User user)
        {
            updatedUser.Name = user.Name;
            await _unitOfWork.CommitAsync();
        }
    }
}
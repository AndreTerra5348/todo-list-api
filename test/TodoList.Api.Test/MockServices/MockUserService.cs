using Moq;
using TodoList.Core.Models;
using TodoList.Core.Services;

namespace TodoList.Api.Test.MockServices
{
    public class MockUserService : Mock<IUserService>
    {
        public MockUserService MockGetAllAsync(params User[] output)
        {
            Setup(x => x.GetAllAsync())
                .ReturnsAsync(output);
            return this;
        }

        public MockUserService MockGetByIdAsync(User output)
        {
            Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(output);
            return this;
        }

        public MockUserService MockCreateAsync(User output)
        {
            Setup(x => x.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(output);
            return this;
        }

        public MockUserService MockUpdateAsync()
        {
            Setup(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<User>()));
            return this;
        }

        public MockUserService MockDeleteAsync()
        {
            Setup(x => x.DeleteAsync(It.IsAny<User>()));
            return this;
        }
    }
}
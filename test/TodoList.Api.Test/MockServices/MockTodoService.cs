using System.Collections.Generic;
using Moq;
using TodoList.Core.Models;
using TodoList.Core.Services;

namespace TodoList.Api.Test.MockServices
{
    internal class MockTodoService : Mock<ITodoService>
    {
        public MockTodoService MockGetAllWithUserAsync(params Todo[] output)
        {
            Setup(x => x.GetAllWithUserAsync())
                .ReturnsAsync(output);
            return this;
        }

        public MockTodoService MockGetByIdAsync(Todo output)
        {
            Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(output);
            return this;
        }

        public MockTodoService MockGetByUserIdAsync(params Todo[] output)
        {
            Setup(x => x.GetByUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync(output);
            return this;
        }

        public MockTodoService MockCreateAsync(Todo output)
        {
            Setup(x => x.CreateAsync(It.IsAny<Todo>()))
                .ReturnsAsync(output);
            return this;
        }

        public MockTodoService MockUpdateAsync()
        {
            Setup(x => x.UpdateAsync(It.IsAny<Todo>(), It.IsAny<Todo>()));
            return this;
        }

        public MockTodoService MockDeleteAsync()
        {
            Setup(x => x.DeleteAsync(It.IsAny<Todo>()));
            return this;
        }
    }
}
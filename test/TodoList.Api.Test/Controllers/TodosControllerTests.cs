using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoList.Api.Controllers;
using TodoList.Api.Dtos;
using TodoList.Api.Profiles;
using TodoList.Api.Test.MockServices;
using TodoList.Core.Models;
using Xunit;

namespace TodoList.Api.Test.Controllers
{
    public class TodosControllerTests
    {

        // Get Mapper with profile
        private readonly IMapper _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<TodoListProfile>();
        }).CreateMapper();

        [Fact]
        public async void Get_CallGetAllWithUserAsync_OnlyOnce()
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockGetAllWithUserAsync();

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            await sut.Get();

            //Assert
            mockTodoService.Verify(x => x.GetAllWithUserAsync(), Times.Once);
        }

        [Theory]
        [InlineData(1, "Test", false, 1)]
        public async void Get_CallGetAllWithUserAsync_ReturnOkWith_TodoReadDtoList_WithCorrectValues(int todoId, string title, bool isDone, int userId)
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockGetAllWithUserAsync(new Todo()
                {
                    Id = todoId,
                    Title = title,
                    IsDone = isDone,
                    UserId = userId
                });

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            var result = await sut.Get();

            //Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var todos = okResult.Value.Should().BeAssignableTo<List<TodoReadDto>>().Subject;
            todos.Should().HaveCount(1);
            todos.Should().Contain(t => t.Id == todoId && t.Title == title && t.IsDone == isDone && t.UserId == userId);
        }

        [Theory]
        [InlineData(1)]
        public async void GetTodoById_CallGetByIdAsync_OnlyOnce(int todoId)
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockGetByIdAsync(null);

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            await sut.GetTodoById(todoId);

            //Assert
            mockTodoService.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }


        [Theory]
        [InlineData(1, "Test", false, 1)]
        public async void GetTodoById_CallGetByIdAsync_ReturnOk_WithTodoReadDto_WithCorrectValues(int todoId, string title, bool isDone, int userId)
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockGetByIdAsync(new Todo()
                {
                    Id = todoId,
                    Title = title,
                    IsDone = isDone,
                    UserId = userId
                });

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            var result = await sut.GetTodoById(todoId);

            //Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var todo = okResult.Value.Should().BeAssignableTo<TodoReadDto>().Subject;
            todo.Id.Should().Be(todoId);
            todo.Title.Should().Be(title);
            todo.IsDone.Should().Be(isDone);
            todo.UserId.Should().Be(userId);
        }

        [Theory]
        [InlineData(1)]
        public async void GetTodoById_CallGetByIdAsync_ReturnNotFound(int todoId)
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockGetByIdAsync(null);

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            var result = await sut.GetTodoById(todoId);

            //Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Theory]
        [InlineData(1)]
        public async void GetByUserId_CallGetByUserIdAsync_OnlyOnce(int userId)
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockGetByUserIdAsync(null);

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            await sut.GetByUserId(userId);

            //Assert
            mockTodoService.Verify(x => x.GetByUserIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineData(1, "Test", false, 1)]
        public async void GetByUserId_CallGetByUserIdAsync_ReturnOk_WithTodoReadDto_WithCorrectValues(int todoId, string title, bool isDone, int userId)
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockGetByUserIdAsync(new Todo()
                {
                    Id = todoId,
                    Title = title,
                    IsDone = isDone,
                    UserId = userId
                });

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            var result = await sut.GetByUserId(userId);

            //Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var todo = okResult.Value.Should().BeAssignableTo<List<TodoReadDto>>().Subject.First();
            todo.Id.Should().Be(todoId);
            todo.Title.Should().Be(title);
            todo.IsDone.Should().Be(isDone);
            todo.UserId.Should().Be(todoId);
        }

        [Theory]
        [InlineData(1, "Test", false, 1)]
        public async void Create_CallCreateAsync_OnlyOnce(int todoId, string title, bool isDone, int userId)
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockCreateAsync(new Todo() { Id = todoId })
                .MockGetByIdAsync(new Todo() { Id = todoId });

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            await sut.Create(new TodoCreateDto() { Title = title, IsDone = isDone, UserId = userId });

            //Assert
            mockTodoService.Verify(x => x.CreateAsync(It.IsAny<Todo>()), Times.Once);
        }

        [Theory]
        [InlineData(1, "Test", false, 1)]
        public async void Create_CallCreateAsync_ReturnOk_WithTodoReadDto_WithCorrectValues(int todoId, string title, bool isDone, int userId)
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockCreateAsync(new Todo()
                {
                    Id = todoId,
                    Title = title,
                    IsDone = isDone,
                    UserId = userId
                })
                .MockGetByIdAsync(new Todo()
                {
                    Id = todoId,
                    Title = title,
                    IsDone = isDone,
                    UserId = userId
                });

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            var result = await sut.Create(new TodoCreateDto()
            {
                Title = title,
                IsDone = isDone,
                UserId = userId
            });

            //Assert
            var okResult = result.Result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            var todo = okResult.Value.Should().BeAssignableTo<TodoReadDto>().Subject;
            todo.Id.Should().Be(todoId);
            todo.Title.Should().Be(title);
            todo.IsDone.Should().Be(isDone);
            todo.UserId.Should().Be(userId);
        }

        [Fact]
        public async void Create_CallCreateAsync_BadRequest()
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockCreateAsync(null);

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            var result = await sut.Create(new TodoCreateDto());

            //Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Theory]
        [InlineData(1, "Test", false, 1)]
        public async void Update_CallUpdateAsync_OnlyOnce(int todoId, string title, bool isDone, int userId)
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockUpdateAsync()
                .MockGetByIdAsync(new Todo() { Id = todoId, UserId = userId });

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            await sut.Update(todoId, new TodoCreateDto() { Title = title, IsDone = isDone, UserId = userId });

            //Assert
            mockTodoService.Verify(x => x.UpdateAsync(It.IsAny<Todo>(), It.IsAny<Todo>()), Times.Once);
        }

        [Theory]
        [InlineData(1, "Test", false, 1)]
        public async void Update_CallUpdateAsync_ReturnNoContent(int todoId, string title, bool isDone, int userId)
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockUpdateAsync()
                .MockGetByIdAsync(new Todo() { Id = todoId, UserId = userId });

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            var result = await sut.Update(todoId, new TodoCreateDto() { Title = title, IsDone = isDone, UserId = userId });

            //Assert
            result.Result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async void Update_CallUpdateAsync_BadRequest()
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockUpdateAsync();

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            var result = await sut.Update(1, new TodoCreateDto());

            //Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Theory]
        [InlineData(1, "Test", false, 1)]
        public async void Update_CallUpdateAsync_NotFound(int todoId, string title, bool isDone, int userId)
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockUpdateAsync();

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            var result = await sut.Update(todoId, new TodoCreateDto() { Title = title, IsDone = isDone, UserId = userId });

            //Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Theory]
        [InlineData(1)]
        public async void Delete_CallDeleteAsync_OnlyOnce(int todoId)
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockDeleteAsync()
                .MockGetByIdAsync(new Todo() { Id = todoId });

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            await sut.Delete(todoId);

            //Assert
            mockTodoService.Verify(x => x.DeleteAsync(It.IsAny<Todo>()), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        public async void Delete_CallDeleteAsync_ReturnNoContent(int todoId)
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockDeleteAsync()
                .MockGetByIdAsync(new Todo() { Id = todoId });

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            var result = await sut.Delete(todoId);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async void Delete_CallDeleteAsync_NotFound()
        {
            //Arrange
            var mockTodoService = new MockTodoService()
                .MockDeleteAsync();

            var sut = new TodosController(mockTodoService.Object, _mapper);

            //Act
            var result = await sut.Delete(1);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}

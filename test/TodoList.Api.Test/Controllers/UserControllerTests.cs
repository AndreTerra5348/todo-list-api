using System.Collections.Generic;
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
    public class UserControllerTests
    {
        // Get Mapper with profile
        private readonly IMapper _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<TodoListProfile>();
        }).CreateMapper();

        [Fact]
        public async void Get_CallGetAllAsync_OnlyOnce()
        {
            // Arrange
            var mockUserService = new MockUserService()
                .MockGetAllAsync();

            var sut = new UserController(mockUserService.Object, _mapper);

            // Act
            var result = await sut.Get();

            // Assert
            mockUserService.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Theory]
        [InlineData(1, "User")]
        public async void Get_CallGetAllAsync_ReturnsOk_WithUserReadDtoList_WithCorrectValues(int id, string name)
        {
            // Arrange
            var mockUserService = new MockUserService()
                .MockGetAllAsync(new User { Id = id, Name = name });
            var sut = new UserController(mockUserService.Object, _mapper);

            // Act
            var result = await sut.Get();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var users = okResult.Value.Should().BeAssignableTo<IEnumerable<UserReadDto>>().Subject;
            users.Should().HaveCount(1);
            users.Should().Contain(u => u.Id == id && u.Name == name);
        }

        [Theory]
        [InlineData(1, "User")]
        public async void GetById_CallGetByIdAsync_OnlyOnce(int id, string name)
        {
            // Arrange
            var mockUserService = new MockUserService()
                .MockGetByIdAsync(new User { Id = id, Name = name });

            var sut = new UserController(mockUserService.Object, _mapper);

            // Act
            var result = await sut.GetById(id);

            // Assert
            mockUserService.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Theory]
        [InlineData(1, "User")]
        public async void GetById_CallGetByIdAsync_ReturnsOk_WithUserReadDto_WithCorrectValues(int id, string name)
        {
            // Arrange
            var mockUserService = new MockUserService()
                .MockGetByIdAsync(new User { Id = id, Name = name });
            var sut = new UserController(mockUserService.Object, _mapper);

            // Act
            var result = await sut.GetById(id);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var user = okResult.Value.Should().BeAssignableTo<UserReadDto>().Subject;
            user.Id.Should().Be(id);
            user.Name.Should().Be(name);
        }

        [Theory]
        [InlineData(1)]
        public async void GetById_CallGetByIdAsync_ReturnsNotFound_WhenUserNotFound(int id)
        {
            // Arrange
            var mockUserService = new MockUserService()
                .MockGetByIdAsync(null);
            var sut = new UserController(mockUserService.Object, _mapper);

            // Act
            var result = await sut.GetById(id);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Theory]
        [InlineData(1, "User")]
        public async void Create_CallCreateAsync_OnlyOnce(int id, string name)
        {
            // Arrange
            var mockUserService = new MockUserService()
                .MockCreateAsync(new User { Id = id, Name = name })
                .MockGetByIdAsync(new User { Id = id, Name = name });

            var sut = new UserController(mockUserService.Object, _mapper);
            var userCreateDto = new UserCreateDto { Name = name };

            // Act
            var result = await sut.Create(userCreateDto);

            // Assert
            mockUserService.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Once);
        }

        [Theory]
        [InlineData(1, "User")]
        public async void Create_CallCreateAsync_ReturnsCreatedAtRoute_WithUserReadDto_WithCorrectValues(int id, string name)
        {
            // Arrange
            var mockUserService = new MockUserService()
                .MockCreateAsync(new User { Id = id, Name = name })
                .MockGetByIdAsync(new User { Id = id, Name = name });
            var sut = new UserController(mockUserService.Object, _mapper);
            var userCreateDto = new UserCreateDto { Name = name };

            // Act
            var result = await sut.Create(userCreateDto);

            // Assert
            var createdResult = result.Result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            var user = createdResult.Value.Should().BeAssignableTo<UserReadDto>().Subject;
            user.Id.Should().Be(id);
            user.Name.Should().Be(name);
        }

        [Theory]
        [InlineData(1, "User")]
        public async void Update_CallUpdateAsync_OnlyOnce(int id, string name)
        {
            // Arrange
            var mockUserService = new MockUserService()
                .MockUpdateAsync()
                .MockGetByIdAsync(new User { Id = id, Name = name });

            var sut = new UserController(mockUserService.Object, _mapper);
            var userCreateDto = new UserCreateDto { Name = name };

            // Act
            var result = await sut.Update(id, userCreateDto);

            // Assert
            mockUserService.Verify(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<User>()), Times.Once);
        }

        [Theory]
        [InlineData(1, "User")]
        public async void Update_CallUpdateAsync_ReturnsNoContent_WhenUserUpdated(int id, string name)
        {
            // Arrange
            var mockUserService = new MockUserService()
                .MockUpdateAsync()
                .MockGetByIdAsync(new User { Id = id, Name = name });
            var sut = new UserController(mockUserService.Object, _mapper);
            var userCreateDto = new UserCreateDto { Name = name };

            // Act
            var result = await sut.Update(id, userCreateDto);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Theory]
        [InlineData(1)]
        public async void Update_CallUpdateAsync_ReturnsNotFound_WhenUserNotFound(int id)
        {
            // Arrange
            var mockUserService = new MockUserService()
                .MockUpdateAsync()
                .MockGetByIdAsync(null);
            var sut = new UserController(mockUserService.Object, _mapper);
            var userCreateDto = new UserCreateDto { Name = "User" };

            // Act
            var result = await sut.Update(id, userCreateDto);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Theory]
        [InlineData(1, "User")]
        public async void Delete_CallDeleteAsync_OnlyOnce(int id, string name)
        {
            // Arrange
            var mockUserService = new MockUserService()
                .MockDeleteAsync()
                .MockGetByIdAsync(new User { Id = id, Name = name });

            var sut = new UserController(mockUserService.Object, _mapper);

            // Act
            var result = await sut.Delete(id);

            // Assert
            mockUserService.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Once);
        }

        [Theory]
        [InlineData(1, "User")]
        public async void Delete_CallDeleteAsync_ReturnsNoContent_WhenUserDeleted(int id, string name)
        {
            // Arrange
            var mockUserService = new MockUserService()
                .MockDeleteAsync()
                .MockGetByIdAsync(new User { Id = id, Name = name });
            var sut = new UserController(mockUserService.Object, _mapper);

            // Act
            var result = await sut.Delete(id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Theory]
        [InlineData(1)]
        public async void Delete_CallDeleteAsync_ReturnsNotFound_WhenUserNotFound(int id)
        {
            // Arrange
            var mockUserService = new MockUserService()
                .MockDeleteAsync()
                .MockGetByIdAsync(null);
            var sut = new UserController(mockUserService.Object, _mapper);

            // Act
            var result = await sut.Delete(id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}

